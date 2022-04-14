#include <avr/io.h>
#include <util/delay.h>

#define LED PD6
#define LED_DDR DDRD
#define LED_PORT PORTD

#define BTN PD7
#define BTN_PIN PIND
#define BTN_PORT PORTD

#define DELAY 10

int8_t bufor[13];
uint8_t pointer = 0;

int main() {
  
  BTN_PORT |= _BV(BTN);
  LED_DDR |= _BV(LED); 
  
  while(1) {
  	// Jesli uzytkownik nacisnal przycisk
  	if(!(BTN_PIN & _BV(BTN))) {
  		bufor[pointer/8] |= _BV(pointer%8);
  	}
  	else {
  		bufor[pointer/8] &= ~_BV(pointer%8);
  	}
  	
  	LED_PORT &= ~_BV(LED);
  	
  	uint8_t temp = (pointer + 1)%100;
  	
  	LED_PORT |= ((bufor[temp/8] & _BV(temp%8)) >> (temp%8)) << LED;
  	
  	pointer++;
  	pointer %= 100;
  	_delay_ms(10);
  }
}
