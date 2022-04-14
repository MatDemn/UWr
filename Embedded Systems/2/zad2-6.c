#include <avr/io.h>
#include <util/delay.h>

#include <stdio.h>

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

char TABLE[31] = {
	'?',
	'E',
	'T',
	'I',
	'A',
	'N',
	'M',
	'S',
	'U',
	'R',
	'W',
	'D',
	'K',
	'G',
	'O',
	'H',
	'V',
	'F',
	'?',
	'L',
	'?',
	'P',
	'J',
	'B',
	'X',
	'C',
	'Y',
	'Z',
	'Q',
	'?',
	'?',
};

FILE uart_file;

#define LED PD6
#define LED_DDR DDRD
#define LED_PORT PORTD

#define BTN PD7
#define BTN_PIN PIND
#define BTN_PORT PORTD

int main() {
  // zainicjalizuj UART
  uart_init();
  // skonfiguruj strumienie wejścia/wyjścia
  fdev_setup_stream(&uart_file, uart_transmit, uart_receive, _FDEV_SETUP_RW);
  stdin = stdout = stderr = &uart_file;

  BTN_PORT |= _BV(BTN);					// PULL_UP REZYSTOR
  LED_DDR  |= _BV(LED); 					// STEROWANIE LEDEM
  
  uint8_t spaceStart = 0;					// Licznik czasu dla spacji
  uint8_t index = 0;
  while(1) {
  	if(!(BTN_PIN & _BV(BTN))) {				// Jesli nacisnalem przycisk
  		
  		if(index>30) {
  			_delay_ms(500);
		}
  		_delay_ms(300);
  		if(!(BTN_PIN & _BV(BTN))) { 			// Jesli nadal trzymam przycisk po tym czasie to: -
  			index = 2*index+2;
  			spaceStart = 0;			// Czyszcze licznik dla spacji
  			LED_PORT |= _BV(LED);			// Zapalam lampke, bo interpretuje to jako kreske
  			_delay_ms(500);			// Czekam 500ms (wylonione metoda prob i bledow)
  			LED_PORT &= ~_BV(LED);
  			
  		}
  		else {
  			index = 2*index+1;
  			spaceStart = 0;
  			_delay_ms(400);
  			LED_PORT |= _BV(LED);
  			_delay_ms(100);
  			LED_PORT &= ~_BV(LED);
  		}
  	}
  	else {
  		_delay_ms(50);
  		
  		if(spaceStart < 43)
  			spaceStart += 1;
  			
  		if(index > 0 && spaceStart >= 15) {		// Koniec znaku
  			if(index > 30) {
  				printf("%c", '?');
  				continue;
  			}
  			
  			printf("%c", TABLE[index]);
  			spaceStart = 0;
  			index = 0;
  		}
  		else if(spaceStart == 42) {
  			printf(" ");
  			spaceStart = 43;
  		}
  	}
  }
}
