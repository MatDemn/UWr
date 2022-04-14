#include <avr/io.h>
#include <util/delay.h>
#include <avr/interrupt.h>
#include <stdio.h>
#include <avr/sleep.h>
#include <math.h>

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

// inicjalizacja ADC
void adc_init()
{
  ADMUX   = _BV(REFS0); // referencja 5V
  ADCSRA  = _BV(ADPS2) | _BV(ADPS1) | _BV(ADPS0) | _BV(ADIE); // preskaler 128, przerwania wlaczone
  DIDR0   = _BV(ADC1D) | _BV(ADC2D);	// Wylaczenie cyfrowego wyjscia
  ADCSRA |= _BV(ADEN); // włącz ADC
}

ISR(ADC_vect) {
	// Jeśli to ADC1
	if(ADMUX & _BV(MUX0)) {
		
	}
	// Jeśli to ADC2
	else {
	
	}
}

int main() {
	// zainicjalizuj UART
  	uart_init();
  	// skonfiguruj strumienie wejścia/wyjścia
  	fdev_setup_stream(&uart_file, uart_transmit, uart_receive, _FDEV_SETUP_RW);
  	stdin = stdout = stderr = &uart_file;
  	
  	adc_init();
  	
  	DDRB |= _BV(PB0);
  	
  	sei();

	set_sleep_mode(SLEEP_MODE_ADC);
	while(1) {
		ADMUX = _BV(REFS0) | _BV(MUX0);
		sleep_mode();
		printf("%"PRIu8" Przed: %"PRIu32"mv", PORTB & _BV(PB0), (uint32_t)ADC*5000/1023);
		_delay_ms(5);
		ADMUX = _BV(REFS0) | _BV(MUX1);
		sleep_mode();
		printf(" za: %"PRIu32"mv\r\n", (uint32_t)ADC*5000/1023);
		_delay_ms(5);
		PORTB ^= _BV(PB0);
		_delay_ms(1000);
	}
}


