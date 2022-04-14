#include <avr/io.h>
#include <stdio.h>
#include <avr/interrupt.h>
#include <avr/sleep.h>
#include <util/delay.h>

#define BAUD 9600                          // baudrate
#define UBRR_VALUE ((F_CPU)/16/(BAUD)-1)   // zgodnie ze wzorem

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

uint16_t lastON = 0;
uint16_t lastOFF = 0;
uint8_t  whoFlag = 0;

void timer_init() {
	// TIMER 1
	
	// Non-inverting mode
	// Phase and frequency correct PWM mode TOP=ICR1
	// Prescaller = /256, TOP = 63 500Hz
	TCCR1A = _BV(COM1A1);
	TCCR1B = _BV(WGM13) | _BV(CS12);
	TCCR1C = 0;
	
	// OVF and CAPT interrupt enabled
	TIMSK1 = _BV(ICIE1) | _BV(TOIE1);
	
	// TOP
	ICR1 = 63;
	
	// Filling
	OCR1A = 0;
	
	// Output timera
	DDRB |= _BV(PB1);
	
	// TIMER 0
	
	// CTC mode
	/*
	TCCR0A = _BV(WGM01);
	TCCR0B = _BV(CS02) | _BV(CS00);
	
	TIMSK0 = _BV(TOIE0);
	
	OCR0A*/
}

void adc_init() {
	// ADC0 - Potentiometer, ADC1 - Motor
	// Vcc as reference, ADC0 as Input (measuring potentiometer voltage)
	ADMUX = _BV(REFS0);
	
	// ADC enabled, interrupt enabled, prescaller 64
	ADCSRA = _BV(ADEN) | _BV(ADIE) | _BV(ADPS2) | _BV(ADPS1);
	
	// Digital output 0 and 1 off
	DIDR0 = _BV(ADC1D) | _BV(ADC0D);
}

// Przerwanie dla capture (reached TOP)
ISR(TIMER1_CAPT_vect) {
	whoFlag = 0;
	// Change input to motor (MUX3..0 = 0001)
	ADMUX |= _BV(MUX0);
	// Start conversion
	ADCSRA |= _BV(ADSC);
}

// Przerwanie dla overflow (reached BOTTOM)
ISR(TIMER1_OVF_vect) {
	whoFlag = 1;
	// Change input to motor (MUX3..0 = 0001)
	ADMUX |= _BV(MUX0);
	// Start conversion
	ADCSRA |= _BV(ADSC);
}

// Przerwanie dla ADC (trzeba sprawdzic co wywolalo pomiar)
ISR(ADC_vect) {
	// Jesli to przerwanie od silnika, wtedy wywoluje od razu pomiar dla potencjometru
	if(ADMUX & _BV(MUX0)) {
		if(whoFlag) {
			lastON = ADC;
		}
		else {
			lastOFF = ADC;
		}
	
		// Change input to potentiometer (MUX3..0 = 0000)
		ADMUX &= ~_BV(MUX0);
		// Start conversion
		ADCSRA |= _BV(ADSC);
	}
	// Jesli to przerwanie od potencjometru
	else {
		OCR1A = ADC*ICR1/1023;
	}
}

int main() {
	// zainicjalizuj UART
  	uart_init();
  	// skonfiguruj strumienie wejścia/wyjścia
  	fdev_setup_stream(&uart_file, uart_transmit, uart_receive, _FDEV_SETUP_RW);
  	stdin = stdout = stderr = &uart_file;
  	
  	adc_init();
  	timer_init();
  	
  	sei();
  	
  	
  	//set_sleep_mode(SLEEP_MODE_IDLE);
	while(1) {
		//sleep_mode();
		_delay_ms(1000);
		printf("%"PRIu32"mV \t %"PRIu32"mv\r\n", ((uint32_t)lastOFF)*5000/1023, ((uint32_t)lastON)*5000/1023);
	}
}

