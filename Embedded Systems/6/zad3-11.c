// Dodaj dźwięki do mojego pliku z sound.c
//#include "sound.c"
#include <avr/io.h>
#include <avr/interrupt.h>
#include <inttypes.h>
#include <avr/pgmspace.h>
#include <avr/sleep.h>

#include "mario.h"

#define SS PB2

void timer_init() {
	// Timer musi miec 8kHz (takie probkowanie ma zapisany dzwiek)
	// Czyli 8k razy na sekundę zmienia się probka w DAC.
	
	
	// Czy to ponizej bedzie potrzebne? Chyba nie, bo to robi DAC...
	
		// Drugi timer (albo ten sam, zobaczymy) musi robic PWM dla buzzera.
		// Jak zmienic te probki na PWM dla buzzera?
		// Normalnie. Po prostu wezme 8-bitowy timer i bede zmienial wypelnienie (chyba...).
	
		// No to potrzebuje w takim razie 2 timerow.
	// Czyli musze miec timer co zmienia probki i tyle. Nic wiecej, nic mniej. Chyba...
	
	
	// Fast PWM TOP=ICR1; Prescaler 8; 8kHz
	TCCR1A = _BV(WGM11);
	
	TCCR1B = _BV(WGM12) | _BV(WGM13) | _BV(CS11);
	ICR1 = 249;
	//TCCR1B = _BV(WGM12) | _BV(WGM13) | _BV(CS11) | _BV(CS12);
	//ICR1 = 15624;
	
	// Wlaczenie przerwan dla overflow
	TIMSK1 = _BV(TOIE1);
	
}

// inicjalizacja SPI
void spi_init()
{
    // ustaw piny MOSI, SCK i ~SS jako wyjścia
    DDRB |= _BV(DDB3) | _BV(DDB5) | _BV(DDB2);
    // włącz SPI w trybie master z zegarem 250 kHz
    SPCR = _BV(SPE) | _BV(MSTR);
    
    PORTB |= _BV(SS);
}

uint16_t index = 0;

// Jesli jest overflow, zmieniam probke. Czyli co 1/8000 sekundy
ISR(TIMER1_OVF_vect) {

	uint8_t part1 = (pgm_read_byte(&sound_pcm[index])) >> 4;
	part1 |= 0x70;
	
	uint8_t part2 = (pgm_read_byte(&sound_pcm[index])) << 4;

	// Rozpoczecie transmisji SPI (pin w dol)
	PORTB &= ~_BV(SS);
	
	// --------------------------------------
	
	// rozpocznij transmisję, wrzuć do wyslania odpowiednie bity
	SPDR = part1;
	
    	// czekaj na ukończenie transmisji
    	while (!(SPSR & _BV(SPIF)));
    	
    	// wyczyść flagę przerwania
    	SPSR |= _BV(SPIF);
    	
    	// --------------------------------------
    	
    	// rozpocznij transmisję, wrzuć do wyslania odpowiednie bity
	SPDR = part2;
    	
    	// czekaj na ukończenie transmisji
    	while (!(SPSR & _BV(SPIF)));
    	
    	// wyczyść flagę przerwania
    	SPSR |= _BV(SPIF);
    	
    	// --------------------------------------
    	
    	// Zakonczenie transmisji SPI (pin w gore)
    	PORTB |= _BV(SS);
    	
    	// Kolejny indeks, normalizacja
    	index++;
    	index %= sound_pcm_len;
}

int main() {
	spi_init();
	timer_init();
	sei();
	set_sleep_mode(SLEEP_MODE_IDLE);
	while(1) {
		sleep_mode();	
	}
	return 0;
}


