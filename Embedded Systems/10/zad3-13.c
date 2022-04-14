#include <avr/io.h>
#include <stdio.h>
#include <avr/interrupt.h>
#include <avr/sleep.h>
#include <util/delay.h>

#define MOTOR_EN PB2
#define MOTOR_F1 PB1
#define MOTOR_F2 PB0

#define MOTOR_DDR DDRB
#define MOTOR_PORT PORTB 

void adc_init() {
	// Vcc as reference, ADC5 as input (because cable management)
	ADMUX = _BV(REFS0) | _BV(MUX2) | _BV(MUX0);
	// ADC enabled, interrupts enabled, prescaller 64
	ADCSRA = _BV(ADEN) | _BV(ADIE) | _BV(ADPS2) | _BV(ADPS1);
	// Digital output disabled on ADC5 pin for power saving
	DIDR0 = _BV(ADC5D);
}

void timer_init() {
	// Clear on compare match, Fast PWM, TOP = ICR1 = 124
	// 500Hz
	TCCR1A = _BV(COM1B1) | _BV(WGM11);
	// Prescaller 256
	TCCR1B = _BV(WGM13) | _BV(WGM12) | _BV(CS12);
	// Empty the C register
	TCCR1C = 0;
	// Top value
	ICR1 = 124;
	// For filling
	OCR1B = 0;
	// Interrupt for Overflow
	TIMSK1 = _BV(TOIE1);
	// Set motor control pins as outputs
	MOTOR_DDR |= _BV(MOTOR_EN) | _BV(MOTOR_F1) | _BV(MOTOR_F2);
	// Set all zeros
	MOTOR_PORT &= ~_BV(MOTOR_EN) | ~_BV(MOTOR_F1) | ~_BV(MOTOR_F2);
}

uint16_t motorVal = 0;

// For timer overflow
ISR(TIMER1_OVF_vect) {
	// W lewa strone
	if(ADC<511) {
		MOTOR_PORT |= _BV(MOTOR_F1);
		MOTOR_PORT &= ~_BV(MOTOR_F2);
		
		motorVal = (-(int32_t)ADC*ICR1)/511 + ICR1;
	}
	// W prawa strone
	else {
		MOTOR_PORT |= _BV(MOTOR_F2);
		MOTOR_PORT &= ~_BV(MOTOR_F1);
		
		motorVal = (((uint32_t)ADC+1)*ICR1)/512 - ICR1;
	}
	//printf("%"PRIu16"\r\n", motorVal);
	OCR1B = motorVal;
}

// For ADC conversion finish
ISR(ADC_vect) {
	motorVal = ADC;
}

int main() {
	adc_init();
	timer_init();
	sei();
	
	set_sleep_mode(SLEEP_MODE_ADC);
	while(1) {
		sleep_mode();
	}
}
