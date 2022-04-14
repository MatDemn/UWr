#include <avr/io.h>
#include <stdio.h>
#include <inttypes.h>

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

#define MYVAR int32_t
//#define SCANFORM "f"
//#define PRINTFORM "f"
#define SCANFORM SCNd32
#define PRINTFORM PRId32

int main()
{
  // zainicjalizuj UART
  uart_init();
  // skonfiguruj strumienie wejścia/wyjścia
  fdev_setup_stream(&uart_file, uart_transmit, uart_receive, _FDEV_SETUP_RW);
  stdin = stdout = stderr = &uart_file;
  // program testowy
  while(1) {
	  MYVAR a = 1;
	  MYVAR b = 1;
	  printf("Wpisz pierwsza liczbe:\r\n");
	  scanf("%"SCANFORM, &a);
	  printf("%"PRINTFORM"\r\n", a);
	  
	  printf("Wpisz druga liczbe:\r\n");
	  scanf("%"SCANFORM, &b);
	  printf("%"PRINTFORM"\r\n", b);
	  
	  printf("Dodawanie (%"PRINTFORM" + %"PRINTFORM" = %"PRINTFORM")\r\n", a, b, a+b);
	  printf("Odejmowanie (%"PRINTFORM" - %"PRINTFORM" = %"PRINTFORM")\r\n", a, b, a-b);
	  if(b != 0)
	  	printf("Dzielenie (%"PRINTFORM" / %"PRINTFORM" = %"PRINTFORM")\r\n", a, b, a/b);
	  else
	  	printf("Mianownik nie moze byc rowny zero!\r\n");
  }
}

