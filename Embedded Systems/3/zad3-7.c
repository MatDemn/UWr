#include <avr/io.h>
#include <stdio.h>
#include <inttypes.h>
#include <util/delay.h>

#define BAUD 9600                          // baudrate
#define UBRR_VALUE ((F_CPU)/16/(BAUD)-1)   // zgodnie ze wzorem

#define LED PD2
#define LED_DDR DDRD
#define LED_PORT PORTD

// Funkcja czekajaca, ktora przyjmuje zmienna
void delay_us(uint32_t delay) {
	for(uint32_t i = 0; i<delay; i+=2) {
		_delay_us(2);
	}
}

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

// inicjalizacja ADC
void adc_init()
{
  ADMUX   = _BV(REFS0) | _BV(MUX2) | _BV(MUX1) | _BV(MUX0); // referencja AVcc, wejście ADC7
  //DIDR0   = _BV(ADC0D); // wyłącz wejście cyfrowe (ale ADC7 nie ma cyfrowego, wiec niepotrzebne)
  // częstotliwość zegara ADC 125 kHz (16 MHz / 128)
  ADCSRA  = _BV(ADPS2) | _BV(ADPS1); // preskaler 64
  ADCSRA |= _BV(ADEN); // włącz ADC
}

FILE uart_file;

int main()
{
  LED_DDR |= _BV(LED);		// Przygotowanie ddr dla LED
  // zainicjalizuj UART
  uart_init();
  // skonfiguruj strumienie wejścia/wyjścia
  fdev_setup_stream(&uart_file, uart_transmit, uart_receive, _FDEV_SETUP_RW);
  stdin = stdout = stderr = &uart_file;
  // zainicjalizuj ADC
  adc_init();
  // mierz napięcie
  while(1) {
    
    LED_PORT &= ~_BV(LED);			// Gasze LED
    ADCSRA |= _BV(ADSC); // wykonaj konwersję
    while (!(ADCSRA & _BV(ADIF))); // czekaj na wynik
    ADCSRA |= _BV(ADIF); // wyczyść bit ADIF (pisząc 1!)
    uint16_t v = ADC; // weź zmierzoną wartość (0..1023)
    
    // Wartosc z ADC jest z [0;1023]. Skaluje ja odpowiednio do [0;16]
    // i uzyskuje cos ze zbioru {1, 2, 4, 8, 16, 32, 64, 128, ... , 32768}
    // zeby miec logarytmiczna zmiane jasnosci LEDa
    v = _BV(v/64);	
    // Teraz mapuje to na przedzial [1;8000]
    // Powyzej okolo 60Hz czlowiek nie widzi roznicy w miganiu.
    // Wiec jesli bede czekal mniej niz 8000 na pewno nie bedzie widac migania.		
    v = (8000UL*(v-1)/32767UL);
    // Czekam z ledem
    delay_us(v);
    // Zapalam leda
    LED_PORT |= _BV(LED);
    // Na jakis okreslony czas
    delay_us(v);
  }
}

