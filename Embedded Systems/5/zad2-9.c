#include <avr/io.h>
#include <util/delay.h>
#include <avr/interrupt.h>
#include <stdio.h>
#include <avr/sleep.h>

#define BTN PD2
#define BTN_PIN PIND
#define BTN_PORT PORTD

#define BAUD 9600                          // baudrate
#define UBRR_VALUE ((F_CPU)/16/(BAUD)-1)   // zgodnie ze wzorem

// inicjalizacja UART
void uart_init()
{
  // ustaw baudrate
  UBRR0 = UBRR_VALUE;
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

volatile uint16_t measuredADC = 0; 

// inicjalizacja ADC
void adc_init()
{
  ADMUX   = _BV(REFS0); // referencja AVcc
  // częstotliwość zegara ADC 125 kHz (16 MHz / 128)
  ADCSRA  = _BV(ADPS2) | _BV(ADPS1) | _BV(ADATE) | _BV(ADIE); // preskaler 64 i Auto Trigger Enable
  ADCSRB  = _BV(ADTS1); // External Interrupt Request 0 - Przycisk podpiety do INT0
  ADCSRA |= _BV(ADEN); // włącz ADC
}

ISR(ADC_vect) {
	// Zapisuje wynik ADC
	measuredADC = ADC;
}

ISR(INT0_vect) {
	// Puste cialo przerwania (bo nic nie musze w tym przerwaniu robic)
}

FILE uart_file;

int main() {
  // zainicjalizuj UART
  uart_init();
  // skonfiguruj strumienie wejścia/wyjścia
  fdev_setup_stream(&uart_file, uart_transmit, uart_receive, _FDEV_SETUP_RW);
  stdin = stdout = stderr = &uart_file;
  
  
  // ustaw wyzwalanie przerwania na INT0 zboczem schodzacym
  EICRA |= _BV(ISC01);
  // odmaskuj przerwania dla INT0
  EIMSK |= _BV(INT0);
  
  BTN_PORT |= _BV(BTN);	// Pullup Rezystor ON
  adc_init();
  sei();
  
  while(1) {
  	// printf jest dosc spory, wiec w trakcie moze wystapic przerwanie
  	// dlatego chce sie od tego uchronic, wylaczajac przerwania na czas wypisywania
  	// Byc moze bedzie potrzebny sposob opisany w dokumentacji (zeby przerwanie nie zginelo podczas
  	// obslugiwania printf)
  	cli();
  	printf("Ostatnia wartosc: %"PRIu16"\r\n", measuredADC);
  	sei();
  	_delay_ms(1000);
  }
}
