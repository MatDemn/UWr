#include <avr/io.h>
#include <stdio.h>
#include <inttypes.h>
#include <math.h>
#include <avr/delay.h>

#define BAUD 9600                          // baudrate
#define UBRR_VALUE ((F_CPU)/16/(BAUD)-1)   // zgodnie ze wzorem

#define RESIST1 10000.0			// Rezystancja opornika z dzielnika napiecia 
#define BCONST 3300				// Stala termistora (wyznaczona eksperymentalnie)
#define ROOM_TEMP 298.15			// W takiej temperaturze (25stopni) termistor ma okreslony opor
#define ROOM_TEMP_RES 4700			// Taki opor ma w tej temperaturze

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
  ADMUX   = _BV(REFS0); // referencja AVcc, wejście ADC0
  DIDR0   = _BV(ADC0D); // wyłącz wejście cyfrowe na ADC0
  // częstotliwość zegara ADC 125 kHz (16 MHz / 128)
  ADCSRA  = _BV(ADPS0) | _BV(ADPS1) | _BV(ADPS2); // preskaler 128
  ADCSRA |= _BV(ADEN); // włącz ADC
}

FILE uart_file;

int main()
{
  // zainicjalizuj UART
  uart_init();
  // skonfiguruj strumienie wejścia/wyjścia
  fdev_setup_stream(&uart_file, uart_transmit, uart_receive, _FDEV_SETUP_RW);
  stdin = stdout = stderr = &uart_file;
  // zainicjalizuj ADC
  adc_init();
  // mierz napięcie
  while(1) {
    ADCSRA |= _BV(ADSC); // wykonaj konwersję
    while (!(ADCSRA & _BV(ADIF))); // czekaj na wynik
    ADCSRA |= _BV(ADIF); // wyczyść bit ADIF (pisząc 1!)
    uint16_t v = ADC;	// Pobierz wynik do v
    float Resist = RESIST1 * (1023.0/(float)v - 1.0);	// Policz rezystancje termistora
    
    // Przelicz na temperature z podanego (przeksztalconego) wzoru
    float KELVINTEMP = (BCONST * ROOM_TEMP)/(BCONST + (ROOM_TEMP * log(Resist/ROOM_TEMP_RES))); 
    // Teraz przelicz na Celsjusze i wypisz na ekran
    printf("Temperatura: %f C\r\n", KELVINTEMP-273.15);
    // Rob tak co sekunde
    _delay_ms(1000);
  }
}

