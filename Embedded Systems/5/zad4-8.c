#include <avr/io.h>
#include <util/delay.h>
#include <avr/interrupt.h>
#include <stdio.h>
#include <avr/sleep.h>

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

FILE uart_file;

// Zmienne do trzymania pomiarow (do liczenia czestotliwosci)
volatile uint16_t measure1 = 0, measure2 = 0;

void timer1_init()
{
  // ustaw tryb licznika
  // COM1A = 10   -- non-inverting mode (tutaj nieistotne, bo nie uzywamy pinow timera)
  // WGM1  = 1111 -- fast PWM top=OCR1A
  // CS1   = 101  -- prescaler 1024
  // ORC1A  = 15624
  // częstotliwość 16e6/(1024*(1+15624)) = 1 Hz
  OCR1A = 15624;
  // Non-inverting mode, fast PWN top=OCR1A, preskaler 1024, ICES1 - Zbocze rosnace uruchamia przerwanie (chyba opcjonalne)
  TCCR1A = _BV(COM1A1) | _BV(WGM11) | _BV(WGM10);
  TCCR1B = _BV(WGM12) | _BV(WGM13) | _BV(CS10) | _BV(CS12) | _BV(ICES1);
  
  // Odmaskowanie przerwan dla input capture
  TIMSK1 = _BV(ICIE1);
}


// Przerwanie dla capture event (kiedy pin dostanie sygnal od oscylatora)
ISR(TIMER1_CAPT_vect) {
	// Trzymam dwa odczyty (z nich moge policzyc czestotliwosc)
	// Dostaje nowy, wiec stary wrzucam do measure1, nowy do measure2
	measure1 = measure2;
	measure2 = ICR1;
	
	// Licze czestotliwosc na podstawie tych pomiarow (bo wiem teraz ile trwa okres jednego drgania oscylatora)
	// Sprawdzam ktory jest mniejszy, bo jeden pomiar moze byc z jednego cyklu timera, drugi z drugiego
	uint16_t difference;
	if(measure1 < measure2) {
		difference = measure2-measure1;
	}
	else {
		difference = measure1-measure2;
	}
	
	// Wypisuje na ekran zmierzona czestotliwosc. Wiem ile trwa jeden cykl oscylatora, wiem ze jedna sekunda to 65535
	// dla timera (ma 1Hz), wiec obliczajac 65535/difference bede mial czestotliwosc. 
	printf("Czestotliwosc: %"PRIu16"\r\n", 65535u/difference);
	
	// Opcjonalnie, zeby lepiej bylo widac wyniki, co sekunde wypisuje wartosci
	_delay_ms(1000);
}
int main() {
  
  // zainicjalizuj UART
  uart_init();
  // skonfiguruj strumienie wejścia/wyjścia
  fdev_setup_stream(&uart_file, uart_transmit, uart_receive, _FDEV_SETUP_RW);
  stdin = stdout = stderr = &uart_file;
  
  timer1_init();
  set_sleep_mode(SLEEP_MODE_IDLE);
  sei();
  
  while(1) {
  	// Jedyne co ma robic petla to usypiac procesor.
  	// Logika programu w obsludze przerwania.
  	sleep_mode();
  }
}
