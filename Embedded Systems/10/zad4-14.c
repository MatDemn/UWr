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

void adc_init() {
	// Vcc as reference, ADC5 as input (because cable management)
	ADMUX = _BV(REFS0) | _BV(MUX2) | _BV(MUX0);
	// ADC enabled, interrupts enabled, prescaller 128
	ADCSRA = _BV(ADEN) | _BV(ADIE) | _BV(ADPS2) | _BV(ADPS1);
	// Digital output disabled on ADC5 pin for power saving
	DIDR0 = _BV(ADC5D);
}

void timer_init() {
	
	// PWM mode, filling from 2/40 to 4/40 (no more, no less, all states between those two!)
	// Prescaller 256, non-inverting mode
	TCCR1A = _BV(COM1A1) | _BV(WGM11);
	TCCR1B = _BV(WGM13) | _BV(WGM12) | _BV(CS12);
	TCCR1C = 0;
	
	// For 50Hz
	ICR1 	 = 1249;
	
	// Rotate -90 at first
	OCR1A 	 = 125;
	
	// PWM pin to output to do a PWM
	DDRB |= _BV(PB1);
	
}

// For ADC conversion finish
ISR(ADC_vect) {
	
	//printf("%"PRIu16"\r\n", OCR1A);
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
	
	set_sleep_mode(SLEEP_MODE_ADC);
	while(1) {
		//OCR1A = 35;
		//_delay_ms(1000);	
		sleep_mode();
		//OCR1A = ((((uint32_t)ADC)*90)/1023 + 35);
		uint16_t temp = ((((uint32_t)ADC)*90)/1023 + 35);
		//printf("%"PRIu16"\r\n", temp);
		OCR1A = temp;
		_delay_ms(10);
	}
}
