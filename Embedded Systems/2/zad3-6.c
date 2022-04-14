#include <avr/io.h>
#include <util/delay.h>

#define BTN3 PB0
#define BTN2 PB1
#define BTN1 PB2
#define LED_PORT PORTD
#define LED_DDR  DDRD

#define BTN_PIN PINB
#define BTN_PORT PORTB

#define DELAY 200

int main() {

  UCSR0B &= ~_BV(RXEN0) & ~_BV(TXEN0);		// Bo uzywam pinow tx  rx
  LED_DDR |= 0xFF;					// Ustawiam wszystkie piny bloku D (uzywam do ledow)
  
  BTN_PORT |= _BV(BTN1);				// Wlaczenie PULL_UP rezystorow
  BTN_PORT |= _BV(BTN2);				//
  BTN_PORT |= _BV(BTN3);				//
  
  uint8_t currentCode = 0;
  LED_PORT = 0;
  
  while(1) {
  	
  	if(!(BTN_PIN & _BV(BTN1))) {				// Jesli wcisniety przycisk PREV
  		_delay_ms(DELAY);
  		if(!(BTN_PIN & _BV(BTN1))) {
  			currentCode -= 1;
  			LED_PORT = 0;
  			LED_PORT |= (currentCode ^ (currentCode >> 1));
  		}
  	}
  	
  	if(!(BTN_PIN & _BV(BTN2))) {				// Jesli wcisniety przycisk RESET
  		_delay_ms(DELAY);
  		if(!(BTN_PIN & _BV(BTN2))) {
  			currentCode = 0;
  			LED_PORT = 0;
  			LED_PORT |= (currentCode ^ (currentCode >> 1));
  		}
  	}
  	
  	if(!(BTN_PIN & _BV(BTN3))) {				// Jesli wcisniety przycisk NEXT
  		_delay_ms(DELAY);
  		if(!(BTN_PIN & _BV(BTN3))) {
  			currentCode += 1;
  			LED_PORT = 0;
  			LED_PORT |= (currentCode ^ (currentCode >> 1));
  		}
  	}
  	
  }
}
