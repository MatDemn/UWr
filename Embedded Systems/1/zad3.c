#include <avr/io.h>
#include <util/delay.h>
#include <inttypes.h>

#define DELAYTIME 100

int main()
{
  // program
  UCSR0B &= ~_BV(RXEN0) & ~_BV(TXEN0);		// Bo uzywam pinow tx  rx
  DDRD |= 0xFF;					// Ustawiam wszystkie piny bloku D
  
  while(1) {
  
  	for(int8_t i = 0; i<8; i++) {
  		_delay_ms(DELAYTIME);
  		PORTD = 0;
  		PORTD |= _BV(i);
  	}
  	
  	for(int8_t i = 6; i>0; i--) {
  		_delay_ms(DELAYTIME);
  		PORTD = 0;
  		PORTD |= _BV(i);
  	}
  }
  
  
}
