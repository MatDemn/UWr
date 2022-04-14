#include <avr/io.h>
#include <stdio.h>
#include <inttypes.h>
#include <util/delay.h>
#include <stdlib.h>

#define BAUD 9600                          // baudrate
#define UBRR_VALUE ((F_CPU)/16/(BAUD)-1)   // zgodnie ze wzorem
#define RAND_MAX 359

#define LED_B PC0
#define LED_G PC1
#define LED_R PC2

#define LED_DDR DDRC
#define LED_PORT PORTC

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

void timer_init()
{
  
  // Timer 0
  // ustaw tryb licznika
  // COM1A = 10   -- non-inverting mode
  // WGM1  = 1110 -- fast PWM top=MAX
  // CS0   = 101  -- prescaler 1024
  // częstotliwość 16e6/(1024*256) =  Hz
  // wzór: datasheet 20.12.3 str. 164
  TCCR0A = _BV(COM0A1) | _BV(COM0B1) | _BV(WGM00) | _BV(WGM01);
  TCCR0B = _BV(CS01);
  // ustaw pin OC0A OC0B jako wyjście
  DDRD |= _BV(PD6) | _BV(PD5);
  
  // Timer 1
  // ustaw tryb licznika
  // COM1A =    -- non-inverting mode
  // WGM1  =  -- fast PWM top=OCR1A
  // CS1   =   -- prescaler 1024
  // częstotliwość 16e6/(1024*(1+x)) =  ~60Hz
  // wzór: datasheet 20.12.3 str. 164
  ICR1 = 259;
  TCCR1A = _BV(COM1A1) | _BV(WGM11);
  TCCR1B = _BV(WGM12) | _BV(WGM13) | _BV(CS12);
  // ustaw pin OC1A (PB1) jako wyjście
  DDRB |= _BV(PB1);
  
  // Timer 2
  // ustaw tryb licznika
  // COM1A = -- non-inverting mode
  // WGM1  = -- fast PWM top=MAX
  // CS0   = -- prescaler 1024
  // częstotliwość 16e6/(1024*256) =  Hz
  // wzór: datasheet 20.12.3 str. 164
  TCCR2A = _BV(COM2A1) | _BV(WGM20) | _BV(WGM21) ;
  TCCR2B = _BV(CS21);
  // ustaw pin jako wyjście
  DDRB |= _BV(PB3);
  
}

FILE uart_file;

void HSVtoRGB(uint16_t HSV, uint8_t *R, uint8_t *G, uint8_t *B) {
	HSV *= 100;
	HSV /= 60;
	
	uint8_t f = HSV%100;
	uint8_t i = HSV-f;
	uint8_t q = 100-f;
	
	if (i<99) 	 {*R = 100; *G = f; *B = 0;}
	else if (i<199) {*R = q; *G = 100; *B = 0;}
	else if (i<299) {*R = 0; *G = 100; *B = f;}
	else if (i<399) {*R = 0; *G = q; *B = 100;}
	else if (i<499) {*R = f; *G = 0; *B = 100;}
	else		 {*R = 100; *G = 0; *B = q;}
}

int main()
{
  LED_DDR |= _BV(LED_R) | _BV(LED_G) | _BV(LED_B);
  // zainicjalizuj UART
  uart_init();
  // skonfiguruj strumienie wejścia/wyjścia
  fdev_setup_stream(&uart_file, uart_transmit, uart_receive, _FDEV_SETUP_RW);
  stdin = stdout = stderr = &uart_file;
  timer_init();
  while(1) {
    
    uint16_t randomNum = rand();
    
    uint8_t redColor = 0;
    uint8_t greenColor = 0;
    uint8_t blueColor = 0;
    
    HSVtoRGB(randomNum, &redColor, &greenColor, &blueColor);
    
    OCR2A = 255-redColor;
    OCR0B = 255-greenColor;
    OCR0A = 255-blueColor;
  	
  	
    for(uint16_t iter = 1; iter <= 259; iter++) {
    	OCR1A = iter;
    	_delay_ms(3);
    }
    for(uint16_t iter = 258; iter >= 1; iter--) {
    	OCR1A = iter;
    	_delay_ms(3);
    }
    
    
  }
}

