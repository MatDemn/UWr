#include <avr/io.h>
#include <util/delay.h>
#include <avr/interrupt.h>
#include <stdio.h>
#include <avr/sleep.h>

#define LED PD7
#define LED_DDR DDRD
#define LED_PORT PORTD

#define BTN PD2
#define BTN_PIN PIND
#define BTN_PORT PORTD

int8_t bufor[13];
uint8_t pointer = 0;

void timer1_init()
{
  // ustaw tryb licznika
  // COM1A = 10   -- non-inverting mode
  // WGM1  = 1110 -- fast PWM top=ICR1
  // CS1   = 101  -- prescaler 1024
  // ICR1  = 155
  // częstotliwość 16e6/(1024*(1+155)) = 100 Hz
  // wzór: datasheet 20.12.3 str. 164
  ICR1 = 155;
  TCCR1A = _BV(COM1A1) | _BV(WGM11);
  TCCR1B = _BV(WGM12) | _BV(WGM13) | _BV(CS10) | _BV(CS12);
  // ustaw pin OC1A (PB1) jako wyjście
  DDRB |= _BV(PB1);
  
  // Odmaskowanie Compare A Interrupt
  TIMSK1 |= _BV(OCIE1A);
}

ISR(TIMER1_COMPA_vect) {
	// Zwiekszam indeks
	pointer++;
	// Normalizuje go (zeby nie wyjsc poza tablice)
	pointer%=100;
	
	// Przycisk nacisniety
	if(!(BTN_PIN & _BV(BTN))) {
  		bufor[pointer/8] |= _BV(pointer%8);
  		//PORTB |= _BV(PB5);
  	}
  	// Przycisk nienacisniety
  	else {
  		bufor[pointer/8] &= ~_BV(pointer%8);
  		//PORTB &= ~_BV(PB5);
  	}
	
	// Obliczam indeks, ktory mam sprawdzic do odtworzenia sygnalu
	uint8_t index = (pointer+1)%100;
	
	// Odtwarzam sygnal
	// Czyszcze najpierw port (zeby led mial zero)
	LED_PORT &= ~_BV(LED);
	// Ustawiam stan leda na taki, jaki wskazuje bit w tablicy)
	LED_PORT |= ((bufor[index/8] & _BV(index%8)) >> (index%8)) << LED;
}

int main() {
  
  DDRB |= _BV(PB5);
  BTN_PORT |= _BV(BTN);	// Pullup Rezystor ON
  LED_DDR |= _BV(LED); 	// Port dla leda na output
  timer1_init();
  set_sleep_mode(SLEEP_MODE_IDLE);
  sei();
  
  while(1) {
  	sleep_mode();
  }
}
