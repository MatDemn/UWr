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
  ADMUX   = _BV(REFS0) | _BV(MUX0); // referencja 5V, mierzenie ADC1
  ADCSRA  = _BV(ADPS2) | _BV(ADPS1) | _BV(ADPS0) | _BV(ADIE); // preskaler 128, przerwania wlaczone
  DIDR0   = _BV(ADC1D);	// Wylaczenie cyfrowego wyjscia
  ADCSRA |= _BV(ADEN); // włącz ADC
}

void timer_init() {
	// TIMER 1
	
	// Non-inverting mode
	// Fast PWM mode TOP=ICR1
	// Prescaller = /8, TOP = 249 , 8000Hz
	TCCR1A = _BV(WGM11);
	TCCR1B = _BV(WGM13) | _BV(WGM12) | _BV(CS11);
	TCCR1C = 0;
	
	// OVF interrupt enabled
	TIMSK1 = _BV(TOIE1);
	
	// TOP
	ICR1 = 249;
	
}

uint32_t countCycles = 0;
uint32_t totalVal = 0;

ISR(TIMER1_OVF_vect) {
	ADCSRA |= _BV(ADSC);
}

ISR(ADC_vect) {
	uint32_t temp = (uint32_t)ADC*500/1023 - 250;
	totalVal += temp*temp;
	countCycles++;
	
	if(countCycles == 1000) {
		float srednia = (sqrt((float)(totalVal/countCycles)));
		printf("%f dBFS\r\n", 20*log10(srednia/220));
		totalVal = 0;
		countCycles = 0;
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

	set_sleep_mode(SLEEP_MODE_IDLE);
	while(1) {
		sleep_mode();
	}
}


