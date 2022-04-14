#include <avr/io.h>
#include <util/delay.h>
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

void timer1_init()
{
  // ustaw tryb licznika
  // WGM1  = 0000 -- normal
  // CS1   = 001  -- prescaler 1
  TCCR1B = _BV(CS10);
}
FILE uart_file;

volatile int8_t  temp1 = 1;
volatile int16_t temp2 = 1;
volatile int32_t temp3 = 1;
volatile int64_t temp4 = 1;
volatile float    temp5 = 1.0;

volatile int8_t x8 = 75;
volatile int8_t y8 = 42;

volatile int16_t x16 = 75;
volatile int16_t y16 = 42;

volatile int32_t x32 = 75;
volatile int32_t y32 = 42;

volatile int64_t x64 = 75;
volatile int64_t y64 = 42;

volatile float xf = 75;
volatile float yf = 42;

int main()
{
  // zainicjalizuj UART
  uart_init();
  // skonfiguruj strumienie wejścia/wyjścia
  fdev_setup_stream(&uart_file, uart_transmit, uart_receive, _FDEV_SETUP_RW);
  stdin = stdout = stderr = &uart_file;
  // zainicjalizuj licznik
  timer1_init();
  // program testowy
    uint16_t start, end; // zmienne dla wyliczonej ilosci cykli; TCNT1 - wartosc
    
    start = TCNT1;
    temp1 = x8 + y8;
    end = TCNT1;
    printf("\tZmierzony INT8+: %"PRIu16" cykli\r\n", end-start);
    
    start = TCNT1;
    temp1 = x8*y8;
    end = TCNT1;
    printf("\tZmierzony INT8*: %"PRIu16" cykli\r\n", end-start);
    
    start = TCNT1;
    temp1 = x8/y8;
    end = TCNT1;
    printf("\tZmierzony INT8/: %"PRIu16" cykli\r\n", end-start);
    
    printf("\r\n");
    
    start = TCNT1;
    temp2 = x16 + y16;
    end = TCNT1;
    printf("\tZmierzony INT16+: %"PRIu16" cykli\r\n", end-start);
    
    start = TCNT1;
    temp2 = x16*y16;
    end = TCNT1;
    printf("\tZmierzony INT16*: %"PRIu16" cykli\r\n", end-start);
    
    start = TCNT1;
    temp2 = x16/y16;
    end = TCNT1;
    printf("\tZmierzony INT16/: %"PRIu16" cykli\r\n", end-start);
    
    printf("\r\n");
    
    start = TCNT1;
    temp3 = x32+y32;
    end = TCNT1;
    printf("\tZmierzony INT32+: %"PRIu16" cykli\r\n", end-start);
    
    start = TCNT1;
    temp3 = x32*y32;
    end = TCNT1;
    printf("\tZmierzony INT32*: %"PRIu16" cykli\r\n", end-start);
    
    start = TCNT1;
    temp3 = x32/y32;
    end = TCNT1;
    printf("\tZmierzony INT32/: %"PRIu16" cykli\r\n", end-start);
    
    printf("\r\n");
    
    start = TCNT1;
    temp4 = x64+y64;
    end = TCNT1;
    printf("\tZmierzony INT64+: %"PRIu16" cykli\r\n", end-start);
    
    start = TCNT1;
    temp4 = x64*y64;
    end = TCNT1;
    printf("\tZmierzony INT64*: %"PRIu16" cykli\r\n", end-start);
    
    start = TCNT1;
    temp4 = x64/y64;
    end = TCNT1;
    printf("\tZmierzony INT64/: %"PRIu16" cykli\r\n", end-start);
    
    printf("\r\n");
    
    start = TCNT1;
    temp5 = xf + yf;
    end = TCNT1;
    printf("\tZmierzony FLOAT+: %"PRIu16" cykli\r\n", end-start);
    
    start = TCNT1;
    temp5 = xf*yf;
    end = TCNT1;
    printf("\tZmierzony FLOAT*: %"PRIu16" cykli\r\n", end-start);

    start = TCNT1;
    temp5 = xf/yf;
    end = TCNT1;
    printf("\tZmierzony FLOAT/: %"PRIu16" cykli\r\n", end-start);
}

