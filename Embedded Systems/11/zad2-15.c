#include <avr/io.h>
#include <stdio.h>
#include <avr/interrupt.h>
#include <avr/sleep.h>
#include <util/delay.h>
#include "pid.h"

#define BAUD 9600                          // baudrate
#define UBRR_VALUE ((F_CPU)/16/(BAUD)-1)   // zgodnie ze wzorem

/*! \brief P, I and D parameter values
 *
 * The K_P, K_I and K_D values (P, I and D gains)
 * need to be modified to adapt to the application at hand
 */
//! \xrefitem todo "Todo" "Todo list"
#define K_P     0.60
//! \xrefitem todo "Todo" "Todo list"
#define K_I     0.01
//! \xrefitem todo "Todo" "Todo list"
#define K_D     0.7

//! Parameters for regulator
struct PID_DATA pidData;

// inicjalizacja UART
void uart_init()
{
  // ustaw baudrate
  UBRR0 = UBRR_VALUE;
  // wyczyść rejestr UCSR0A
  UCSR0A = 0;
  // włącz odbiornik i nadajnik
  UCSR0B = _BV(RXEN0) | _BV(TXEN0);
  // ustaw format 8n1
  UCSR0C = _BV(UCSZ00) | _BV(UCSZ01);
}

// transmisja jednego znaku
int uart_transmit(char data, FILE *stream)
{
  // czekaj aż transmiter gotowy
  while(!(UCSR0A & _BV(UDRE0)));
  UDR0 = data;
  return 0;
}

// odczyt jednego znaku
int uart_receive(FILE *stream)
{
  // czekaj aż znak dostępny
  while (!(UCSR0A & _BV(RXC0)));
  return UDR0;
}

FILE uart_file;

uint16_t measuredVal = 0;
uint16_t referenceVal = 0;

void timer_init() {
	// TIMER 1 (motor and potentiometer measure)
	
	// Non-inverting mode
	// Phase and frequency correct PWM mode TOP=ICR1
	// Prescaller = /64, TOP = 250 500Hz
	TCCR1A = _BV(COM1A1);
	TCCR1B = _BV(WGM13) | _BV(CS10) | _BV(CS11);
	TCCR1C = 0;
	
	// OVF interrupt enabled
	TIMSK1 = _BV(TOIE1);
	
	// TOP
	ICR1 = 250;
	
	// Filling
	OCR1A = 0;
	
	// Output timera
	DDRB |= _BV(PB1);
}

void adc_init() {
	// ADC0 - Potentiometer, ADC1 - Motor
	// Vcc as reference, ADC0 as Input (measuring potentiometer voltage)
	ADMUX = _BV(REFS0);
	
	// ADC enabled, interrupt enabled, prescaller 128
	ADCSRA = _BV(ADEN) | _BV(ADIE) | _BV(ADPS2) | _BV(ADPS1) ;
	
	// Digital output 0 and 1 off
	DIDR0 = _BV(ADC1D) | _BV(ADC0D);
}

// Przerwanie dla ovf (reached ...)
ISR(TIMER1_OVF_vect) {
	// Change input to motor (MUX3..0 = 0001)
	ADMUX |= _BV(MUX0);
	// Start conversion
	ADCSRA |= _BV(ADSC);
}

// Przerwanie dla ADC (trzeba sprawdzic co wywolalo pomiar)
ISR(ADC_vect) {
	// Jesli to przerwanie od silnika, wtedy wywoluje od razu pomiar dla potencjometru
	if(ADMUX & _BV(MUX0)) {
		
		// Save measured value
		measuredVal = ((1023 - (uint32_t)ADC)*ICR1/1023);
		// Change input to potentiometer (MUX3..0 = 0000)
		ADMUX &= ~_BV(MUX0);
		// Start conversion for potentiometer
		ADCSRA |= _BV(ADSC);
	}
	// Jesli to przerwanie od potencjometru
	else {
		// Save reference value
		referenceVal = (uint32_t)ADC*ICR1/1023;
		
		
		// Change value from PID to good value for motor
		int16_t inputValue = pid_Controller(referenceVal, measuredVal, &pidData);
		
		
		
		// If motor should use brakes, then 0 filling
		if(inputValue <= 0 ) {
			OCR1A = 0;
		}
		// If motor should go very fast, use max filling
		else if (inputValue > ICR1) {
			OCR1A = ICR1;
		}
		// Otherwise
		else {
			OCR1A = inputValue;
		}
		
		printf("%"PRId16"\r\n", inputValue);
		
	}
}

int main() {
	// zainicjalizuj UART
  	uart_init();
  	// skonfiguruj strumienie wejścia/wyjścia
  	fdev_setup_stream(&uart_file, uart_transmit, uart_receive, _FDEV_SETUP_RW);
  	stdin = stdout = stderr = &uart_file;
  	
  	pid_Init(K_P * SCALING_FACTOR, K_I * SCALING_FACTOR , K_D * SCALING_FACTOR , &pidData);
  	
  	adc_init();
  	timer_init();
  	
  	sei();
  	
  	set_sleep_mode(SLEEP_MODE_IDLE);
	while(1) {
		sleep_mode();
		//_delay_ms(1000);
		//printf("%"PRIu16"mV \t %"PRIu16"mv\r\n", referenceVal, measuredVal);
	}
}

