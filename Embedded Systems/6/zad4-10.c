#include <avr/io.h>
#include <stdio.h>
#include <inttypes.h>
#include <avr/interrupt.h>
#include <avr/sleep.h>

uint8_t number = 0;

#define LA_PIN PB1
#define OE_PIN PB2

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

void timer_init() {
	// FAST PWM, TOP=ICR1, prescaler 1024
	TCCR1A = _BV(WGM11);
	TCCR1B = _BV(WGM13) | _BV(WGM12) | _BV(CS12) | _BV(CS10);
	
	// 1Hz
	ICR1 = 15624;
	
	// Odmaskuj przerwania dla overflow
	TIMSK1 = _BV(TOIE1);
}



// inicjalizacja SPI
void spi_init()
{
    // ustaw piny MOSI, SCK i ~SS jako wyjścia
    DDRB |= _BV(DDB3) | _BV(DDB5) | _BV(DDB2);
    // włącz SPI w trybie master z zegarem 250 kHz
    SPCR = _BV(SPE) | _BV(MSTR) | _BV(SPR1);
}

// transfer jednego bajtu
uint8_t spi_transfer(uint8_t data)
{
    // rozpocznij transmisję
    SPDR = data;
    // czekaj na ukończenie transmisji
    while (!(SPSR & _BV(SPIF)));
    // wyczyść flagę przerwania
    SPSR |= _BV(SPIF);
    // zwróć otrzymane dane
    return SPDR;
}


ISR(TIMER1_OVF_vect) {
	// Wysylam numer
  	PORTB |= _BV(LA_PIN);
  	spi_transfer(~DIGITS[number]);
  	number++;
  	number%=10;
}

int main()
{
  timer_init();
  // zainicjalizuj SPI
  spi_init();
  // program testujący połączenie
  
  
  
  // Piny sterujace wyswietlaczem jako wyjscia
  DDRB |= _BV(LA_PIN) | _BV(OE_PIN);

  // Wlaczam wyswietlacz przez wpisanie zera (SS/CS)
  PORTB &= ~_BV(OE_PIN);
  sei();
  set_sleep_mode(SLEEP_MODE_IDLE);
  while(1) {
  	sleep_mode();
  }
}

