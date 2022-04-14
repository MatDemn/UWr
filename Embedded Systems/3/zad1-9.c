#include <avr/io.h>
#include <avr/pgmspace.h>
#include <util/delay.h>
#include <inttypes.h>
#include <stdio.h>

#define BUZZ PC2
#define BUZZ_DDR DDRC
#define BUZZ_PORT PORTC

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

// Funkcja, ktora czeka i moze przyjac zmienna jako czas czekania w us
void delay_us( uint32_t us ) {
	for(uint32_t i = 0; i<us; i+=10) {
		_delay_us(10);
	}
}

// Funkcja, ktora dostaje czestotliwosc i czas dxwieku i go odtwarza na brzeczyku
void TONE(uint32_t freq, uint32_t length) { 
    // Ile cykli ma miec oscylator brzeczyka (petla)
    uint32_t loopCycles = freq * length / 1000;
    // Jaki ma byc czas polowy okresu dzwieku
    uint32_t delayVal =  1000000 / freq / 2;
    
    // Moze sie trafic pauza, wiec czekam
    if(freq == 0) {
    		delay_us(200 * length); 
    }
    // Albo dzwiek, wiec odtwarzam
    else {
	    for (uint32_t i = 0; i < loopCycles; i++) { 
	      BUZZ_PORT |= _BV(BUZZ); 
	      delay_us(delayVal); 
	      BUZZ_PORT &= ~_BV(BUZZ); 
	      delay_us(delayVal); 
	    }
    }
}

// Makra definiujace czestotliwosci okreslonych dzwiekow
#define NOTE_B0  31
#define NOTE_C1  33
#define NOTE_CS1 35
#define NOTE_D1  37
#define NOTE_DS1 39
#define NOTE_E1  41
#define NOTE_F1  44
#define NOTE_FS1 46
#define NOTE_G1  49
#define NOTE_GS1 52
#define NOTE_A1  55
#define NOTE_AS1 58
#define NOTE_B1  62
#define NOTE_C2  65
#define NOTE_CS2 69
#define NOTE_D2  73
#define NOTE_DS2 78
#define NOTE_E2  82
#define NOTE_F2  87
#define NOTE_FS2 93
#define NOTE_G2  98
#define NOTE_GS2 104
#define NOTE_A2  110
#define NOTE_AS2 117
#define NOTE_B2  123
#define NOTE_C3  131
#define NOTE_CS3 139
#define NOTE_D3  147
#define NOTE_DS3 156
#define NOTE_E3  165
#define NOTE_F3  175
#define NOTE_FS3 185
#define NOTE_G3  196
#define NOTE_GS3 208
#define NOTE_A3  220
#define NOTE_AS3 233
#define NOTE_B3  247
#define NOTE_C4  262
#define NOTE_CS4 277
#define NOTE_D4  294
#define NOTE_DS4 311
#define NOTE_E4  330
#define NOTE_F4  349
#define NOTE_FS4 370
#define NOTE_G4  392
#define NOTE_GS4 415
#define NOTE_A4  440
#define NOTE_AS4 466
#define NOTE_B4  494
#define NOTE_C5  523
#define NOTE_CS5 554
#define NOTE_D5  587
#define NOTE_DS5 622
#define NOTE_E5  659
#define NOTE_F5  698
#define NOTE_FS5 740
#define NOTE_G5  784
#define NOTE_GS5 831
#define NOTE_A5  880
#define NOTE_AS5 932
#define NOTE_B5  988
#define NOTE_C6  1047
#define NOTE_CS6 1109
#define NOTE_D6  1175
#define NOTE_DS6 1245
#define NOTE_E6  1319
#define NOTE_F6  1397
#define NOTE_FS6 1480
#define NOTE_G6  1568
#define NOTE_GS6 1661
#define NOTE_A6  1760
#define NOTE_AS6 1865
#define NOTE_B6  1976
#define NOTE_C7  2093
#define NOTE_CS7 2217
#define NOTE_D7  2349
#define NOTE_DS7 2489
#define NOTE_E7  2637
#define NOTE_F7  2794
#define NOTE_FS7 2960
#define NOTE_G7  3136
#define NOTE_GS7 3322
#define NOTE_A7  3520
#define NOTE_AS7 3729
#define NOTE_B7  3951
#define NOTE_C8  4186
#define NOTE_CS8 4435
#define NOTE_D8  4699
#define NOTE_DS8 4978



// Tablica trzymajaca melodie. Mamy albo czestotliwosc dzwieku, albo 0 (pauza)
static const uint32_t song[] PROGMEM = {
  NOTE_E7, NOTE_E7, 0, NOTE_E7,
  0, NOTE_C7, NOTE_E7, 0,
  NOTE_G7, 0, 0,  0,
  NOTE_G6, 0, 0, 0,			// 16

  NOTE_C7, 0, 0, NOTE_G6,
  0, 0, NOTE_E6, 0,
  0, NOTE_A6, 0, NOTE_B6,
  0, NOTE_AS6, NOTE_A6, 0,		// 32

  NOTE_G6, NOTE_E7, NOTE_G7,
  NOTE_A7, 0, NOTE_F7, NOTE_G7,
  0, NOTE_E7, 0, NOTE_C7,
  NOTE_D7, NOTE_B6, 0, 0,		// 47

  NOTE_C7, 0, 0, NOTE_G6,
  0, 0, NOTE_E6, 0,
  0, NOTE_A6, 0, NOTE_B6,
  0, NOTE_AS6, NOTE_A6, 0,

  NOTE_G6, NOTE_E7, NOTE_G7,
  NOTE_A7, 0, NOTE_F7, NOTE_G7,
  0, NOTE_E7, 0, NOTE_C7,
  NOTE_D7, NOTE_B6, 0, 0,
  
  NOTE_G7, NOTE_FS7, NOTE_F7, NOTE_D7, 0, NOTE_E7, 0,
  NOTE_G6, NOTE_A6, NOTE_C7, 0,
  NOTE_A6, NOTE_C7, NOTE_D7, 0,
  NOTE_G7, NOTE_FS7, NOTE_F7, NOTE_D7, 0, NOTE_E7, 0,
  NOTE_C8, 0, NOTE_C8, NOTE_C8, 0,
  
  NOTE_G7, NOTE_FS7, NOTE_F7, NOTE_D7, 0, NOTE_E7, 0,
  NOTE_G6, NOTE_A6, NOTE_C7, 0,
  NOTE_A6, NOTE_C7, NOTE_D7, 0,
  NOTE_DS7, 0, NOTE_D7, 0, NOTE_C7, 0,
  
  NOTE_C7, NOTE_C7, 0, NOTE_C7, 0,
  NOTE_C7, NOTE_D7, 0, NOTE_E7, NOTE_C7, 0, NOTE_A6, NOTE_G6, 0,
  NOTE_C7, NOTE_C7, 0, NOTE_C7, 0,
  NOTE_C7, NOTE_D7, NOTE_E7, 0,
  
  NOTE_C7, NOTE_C7, 0, NOTE_C7, 0,
  NOTE_C7, NOTE_D7, 0, NOTE_E7, NOTE_C7, 0, NOTE_A6, NOTE_G6, 0
};
// Tempo kazdego dzwieku z tablicy powyzej. Ta wartosc interpretowana jest jako: 1000ms/tempo[i]
static const uint32_t tempo[] PROGMEM = {
  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,		//16

  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,		//32

  9, 9, 9,
  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,		//47

  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,		//63

  9, 9, 9,
  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,		//78
  
  12, 12, 12, 12, 2, 12, 2,
  12, 12, 12, 6,
  12, 12, 12, 6,
  12, 12, 12, 12, 6, 12, 2,
  12, 9, 12, 12, 3,
  
  12, 12, 12, 12, 2, 12, 2,
  12, 12, 12, 6,
  12, 12, 12, 6,
  12, 2, 12, 2, 12, 1,
  
  12, 12, 12, 12, 12,
  12, 12, 12, 12, 12, 12, 12, 12, 1,
  12, 12, 12, 12, 12,
  12, 12, 12, 12,
  
  12, 12, 12, 12, 12,
  12, 12, 12, 12, 12, 12, 12, 12, 1
};

int main() {
  // zainicjalizuj UART
  uart_init();
  // skonfiguruj strumienie wejścia/wyjścia
  fdev_setup_stream(&uart_file, uart_transmit, uart_receive, _FDEV_SETUP_RW);
  stdin = stdout = stderr = &uart_file;

  // Wlaczam sterowanie brzeczykiem
  BUZZ_DDR |= _BV(BUZZ);
  while(1) {
  	  // Musze wiedziec jak duzajest tablica z melodia
	  uint8_t songLen = sizeof(song) / sizeof(uint32_t);
	  // Iteruje po wszystkich dzwiekach melodii
	  for(uint8_t i = 0; i<songLen; i++) {
	  	// Odczytuje czas trwania dzwieku i odpowiednio go ustawiam na czas w ms
	  	uint32_t duration = (uint32_t)1000/pgm_read_dword(&tempo[i]);
	  	// Odtwarzam dzwiek
	  	TONE(pgm_read_dword(&song[i]), duration);
	  	// Zeby byl odstep pomiedzy dzwiekami
	  	_delay_ms(100);
	  }
  }
  
}
