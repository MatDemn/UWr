#include <avr/io.h>
#include <stdio.h>
#include <avr/interrupt.h>
#include <avr/sleep.h>
#include <util/delay.h>

#define BAUD 9600                          // baudrate
#define UBRR_VALUE ((F_CPU)/16/(BAUD)-1)   // zgodnie ze wzorem

#define MOSFET PB5
#define MOSFET_DDR DDRB
#define MOSFET_PORT PORTB

// Nastawiona temperatura na termostacie (300 -> 30 stopni)
uint16_t setTEMP = 280;
// Histeraza (10 -> 1 stopien)
uint8_t hysteresis = 10;
// Zmierzona aktualnie temperatura (*10)
volatile uint16_t measuredTemp = 42;

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


void timer_init() {
	// Fast PWM
	TCCR1A = _BV(WGM11);
	
	// CTC mode, prescaller 1024
	TCCR1B = _BV(WGM13) | _BV(WGM12) | _BV(CS12) | _BV(CS10);
	
	// Timer overflow interrupt enabled
	TIMSK1 = _BV(TOIE1);
	// TOP = ICR1; 1Hz
	ICR1 = 15624;

}

// Inicjalizacja ADC
void adc_init() {
	// Vref as reference, ADC0 as input
	ADMUX = _BV(REFS0) | _BV(REFS1);
	// ADC enable, interrupt enabled, 128 prescaller
	ADCSRA = _BV(ADEN) | _BV(ADIE) | _BV(ADPS2) | _BV(ADPS1) | _BV(ADPS0);
	// Digital input disable (power saving) ADC0
	DIDR0 = _BV(PC0);
}

// Co kazdy overflow timera musze sprawdzic jak sie ma temperatura
// i odpowiednio wlaczyc albo wylaczyc tranzystor MOSFET
ISR(TIMER1_OVF_vect) {
	ADCSRA |= _BV(ADSC); // wykonaj konwersję
}

ISR(ADC_vect) {
	measuredTemp = ((ADC - 465)*100)/99;

	if(measuredTemp > setTEMP) {
		// Wylacz grzalke
		MOSFET_PORT &= ~_BV(MOSFET);
	}
	
	if (measuredTemp < setTEMP - hysteresis){
		// Wlacz grzalke
		MOSFET_PORT |= _BV(MOSFET);
	}
}

char option = 0;
int main() {
	// zainicjalizuj UART
  	uart_init();
  	// skonfiguruj strumienie wejścia/wyjścia
  	fdev_setup_stream(&uart_file, uart_transmit, uart_receive, _FDEV_SETUP_RW);
  	stdin = stdout = stderr = &uart_file;
  	
  	adc_init();
  	timer_init();
  	
  	sei();
  	
  	MOSFET_DDR  |= _BV(MOSFET);
  	
  	// Wylaczam tranzystor na poczatek
  	MOSFET_PORT &= ~_BV(MOSFET);
  	
  	while(1) {
  		printf("---------------\r\n");
  		printf("1 Podaj aktualna temperature \r\n");
  		printf("2 Zmiana temperatury \r\n");
  		
  		option = getchar();
  		
  		switch(option) {
  			case '1':
  				printf("%"PRIu8",%"PRIu8" LIMIT: %"PRIu8",%"PRIu8"\r\n", measuredTemp/10, measuredTemp%10, setTEMP/10, setTEMP%10);
  				break;
  			case '2':
  				scanf("%"SCNu16, &setTEMP);
  				printf("%"PRIu16"\r\n", setTEMP);
  				setTEMP *= 10;
  				break;
  			default:
  				continue;
  		}	
  	}
}
