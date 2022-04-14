#include <avr/io.h>
#include <util/delay.h>
#include <stdio.h>
#include <inttypes.h>

#define BAUD 9600                          // baudrate
#define UBRR_VALUE ((F_CPU)/16/(BAUD)-1)   // zgodnie ze wzorem

#define LED PB5
#define LED_DDR DDRB
#define LED_PORT PORTB

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

#define MYVAR int8_t
#define SCANFORM SCNd8
#define PRINTFORM PRId8
#define LETTERCOUNT 26
#define SYMBOLCOUNT 4
#define INPUTSTRING 20

const int8_t MORSE[LETTERCOUNT][SYMBOLCOUNT] = {
	{0, 1, -1, -1},		//A
	{1, 0, 0, 0},			//B
	{1, 0, 1, 0},			//C
	{1, 0, 0, -1},			//D
	{0, -1, -1, -1},		//E
	{0, 0, 1, 0},			//F	
	{1, 1, 0, -1},			//G
	{0, 0, 0, 0},			//H
	{0, 0, -1, -1},		//I
	{0, 1, 1, 1},			//J
	{1, 0, 1, -1},			//K
	{0, 1, 0, 0},			//L
	{1, 1, -1, -1},		//M
	{1, 0, -1, -1},		//N
	{1, 1, 1, -1},			//O
	{0, 1, 1, 0},			//P
	{1, 1, 0, 1},			//Q
	{0, 1, 0, -1},			//R
	{0, 0, 0, -1},			//S
	{1, -1, -1, -1},		//T
	{0, 0, 1, -1},			//U
	{0, 0, 0, 1},			//V
	{0, 1, 1, -1},			//W
	{1, 0, 0, 1},			//X
	{1, 0, 1, 1},			//Y
	{1, 1, 0, 0}			//Z
};

int main()
{
  // zainicjalizuj UART
  uart_init();
  // skonfiguruj strumienie wejścia/wyjścia
  fdev_setup_stream(&uart_file, uart_transmit, uart_receive, _FDEV_SETUP_RW);
  stdin = stdout = stderr = &uart_file;
  // program testowy
  
  char word[11];
  
  LED_DDR |= _BV(LED);
  
  while(1) {
  	  
	  printf("Podaj slowo do przetlumaczenia na morse'a (max 10 znakow):\r\n");
	  scanf("%10s", word);
	  printf("%s\r\n", word);
	  	
	  word[10] = '\0';
	  	
	  for(char *letter = &word[0]; *letter!='\0'; letter++) {
	  	printf("%c", *letter);
	  	
	  	int8_t temp = (int8_t)(*letter);
	  	
	  	if(temp > 64 && temp < 91)
	  		temp -= 65;
	  	else if(temp > 96 && temp < 123)
	  		temp -= 97;
	  	else {
	  		printf("Wrong character...\r\n");
	  		continue;
	  	}
	  	
	  	for(int8_t i = 0; i<SYMBOLCOUNT; i++) {
	  	
	  		if(MORSE[temp][i] == 0) {
	  			printf("*");
	  			LED_PORT |= _BV(LED);
	    			_delay_ms(300);
	    			LED_PORT &= ~_BV(LED);
		  	}
		  	else if(MORSE[temp][i] == 1) {
		  		printf("-");
		  		LED_PORT |= _BV(LED);
	    			_delay_ms(900);
	    			LED_PORT &= ~_BV(LED);
		  	}
		  	else {
		  		break;
		  	}
		  	_delay_ms(300);
		  	
		  	
		}
	printf("\r\n");
	_delay_ms(900);	  	
	}	
	  /*
	  */
  }
  
  
}
