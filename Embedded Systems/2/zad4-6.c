#include <avr/io.h>
#include <util/delay.h>

#define LED_PORT PORTD
#define LED_DDR  DDRD

#define LEFT_TRANS PC0
#define RIGHT_TRANS PC1

#define BUZZER	PC2

#define TRANS_DDR DDRC
#define TRANS_PORT PORTC

#define TONE(step, delay) \
    for (uint16_t i = 0; i < (uint32_t)1000 * (delay) / (step) / 2; i++) { \
      TRANS_PORT |= _BV(BUZZER); \
      _delay_us(step); \
      TRANS_PORT &= ~_BV(BUZZER); \
      _delay_us(step); \
    }

const int8_t DIGITS[10] = {
	0x40,	//0	ABCDEF- -> -FEDCBA
	0x79,	//1	-BC---- -> ----CB-
	0x24,	//2	AB-DE-G -> G-RD-BA
	0x30,	//3	ABCD--G -> G--DCBA
	0x19,	//4	-BC--FG -> GF--CB-
	0x12,	//5	A-CD-FG -> GF-DC-A
	0x02,	//6	A-CDEFG -> GFEDC-A
	0x78,	//7	ABC---- -> ----CBA
	0x00,	//8	ABCDEFG -> GFEDCBA
	0x10	//9	ABCD-FG -> GF-DCBA
};

int main() {
  uint8_t tenDigit = 2, oneDigit = 0;			// Do trzymania aktualnie wywietlanej liczby dziesiatek i jednosci
  UCSR0B &= ~_BV(RXEN0) & ~_BV(TXEN0);		// Bo uzywam pinow tx  rx
  LED_DDR |= 0xFF;					// Ustawiam wszystkie piny bloku D (uzywam ich do ledow)
  LED_PORT = 0xFF;					// Na starcie wylaczam wszystkie ledy na panelu
  
  TRANS_DDR |= _BV(LEFT_TRANS) | _BV(RIGHT_TRANS);	// Ustawiam sterowanie lewego i prawego tranzystora
  
  TRANS_DDR |= _BV(BUZZER);				// Ustawiam sterowanie dla buzzera
  
  TRANS_PORT &= ~_BV(LEFT_TRANS);			// Wlaczam lewy tranzystor na starcie
  TRANS_PORT |= _BV(RIGHT_TRANS);			// Wylaczam prawy tranzystor na starcie
  
  // W kolejnych krokach bede robil multiplex (wlaczal na zmiane lewa i prawa strone)
  
  while(1) {
  	// Liczenie w gore (0 do 59)
  	/*
  	oneDigit++;
  	tenDigit += oneDigit / 10;

  	oneDigit %= 10;
  	tenDigit %= 6;*/
  
  	// Liczenie w dol (59 do 0)
  	
  	oneDigit--;
  	
  	if(oneDigit == 255) {
  		oneDigit = 9;
  		tenDigit -= 1;
  		if(tenDigit == 255) {
  			tenDigit = 5;
  		}
  	}
  	
  	for(uint8_t i = 0; i<100; i++) {
  		TRANS_PORT ^= _BV(LEFT_TRANS);	// Robie flip pinu sterujacego tranzystorem lewym (0 -> 1, 1 -> 0)
  		TRANS_PORT ^= _BV(RIGHT_TRANS);	// Robie flip pinu sterujacego tranzystorem prawym (0 -> 1, 1 -> 0)
  		
  		if(!(TRANS_PORT & _BV(LEFT_TRANS)))	// Jesli bit LEFT_TRANS jest 1, tranzystor jest wylaczony
  			LED_PORT = DIGITS[tenDigit];
  		else
  			LED_PORT = DIGITS[oneDigit];
  		if(i < 10) {
  			if(tenDigit*10 + oneDigit < 10) {
  				TONE(900, 10);
  			}
  			else {
  				TONE(500, 10);
  			}
  		}
  		else
  			_delay_ms(10);
  	}
  }
}
