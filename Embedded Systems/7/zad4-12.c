#include <avr/io.h>
#include <stdio.h>
#include <inttypes.h>
#include <util/delay.h>
#include <avr/interrupt.h>
#include <avr/sleep.h>

#define BAUD 9600                          // baudrate
#define UBRR_VALUE ((F_CPU)/16/(BAUD)-1)   // zgodnie ze wzorem

#define SLAVE_SS	PB2		// 0
#define SLAVE_MOSI	PB3		// 0
#define SLAVE_MISO	PB4		// 1
#define SLAVE_SCK	PB5		// 0
#define SLAVE_PORT	PORTB
#define SLAVE_DDR	DDRB

#define MASTER_SS	PD4		// 1
#define MASTER_MOSI	PD5		// 1
#define MASTER_MISO	PD6		// 0
#define MASTER_SCK	PD7		// 1
#define MASTER_PORT	PORTD
#define MASTER_DDR	DDRD

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

// inicjalizacja SPI
void spi_init()
{
    // Ustawiam slave
    SLAVE_DDR |= _BV(SLAVE_MISO);

    // Ustawiam mastera 
    MASTER_DDR |= _BV(MASTER_MOSI) | _BV(MASTER_SCK) | _BV(MASTER_SS);
    
    // włącz SPI w trybie slave, interrupts enabled
    SPCR = _BV(SPE) | _BV(SPIE);
}

// Bit banging  wikipedii
void spi_transfer(uint8_t data) {

   // select device (active low)
   MASTER_PORT &= ~_BV(MASTER_SS);

   // send bits 7..0
   for (uint8_t i = 0; i < 8; i++)
   {
       // consider leftmost bit
       // set line high if bit is 1, low if bit is 0
       if (data & 0x80)
           MASTER_PORT |= _BV(MASTER_MOSI);
       else
           MASTER_PORT &= ~_BV(MASTER_MOSI);

       // pulse the clock state to indicate that bit value should be read
       MASTER_PORT |= _BV(MASTER_SCK);
       _delay_us(2);
       MASTER_PORT &= ~_BV(MASTER_SCK);

       // shift byte left so next bit will be leftmost
       data <<= 1;
   }

   // deselect device by writing one
   MASTER_PORT |= _BV(MASTER_SS);
	
}

uint8_t sentData = 0;


ISR(SPI_STC_vect) {
	uint8_t received = SPDR;
	printf("Wyslano: %"PRIu8", odebrano: %"PRIu8"\r\n", sentData, received);
}

int main()
{
  // zainicjalizuj UART
  uart_init();
  // skonfiguruj strumienie wejścia/wyjścia
  fdev_setup_stream(&uart_file, uart_transmit, uart_receive, _FDEV_SETUP_RW);
  stdin = stdout = stderr = &uart_file;
  // zainicjalizuj SPI
  spi_init();
  // program testujący połączenie
  sei();
  //set_sleep_mode(SLEEP_MODE_IDLE);
  while(1) {
  	for(sentData = 0; sentData<=255; sentData++) {
  		spi_transfer(sentData);
  		//sleep_mode();
  		_delay_ms(1000);
  	}
  }
}

