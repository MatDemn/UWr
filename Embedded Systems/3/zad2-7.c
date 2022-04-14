#include <avr/io.h>
#include <stdio.h>
#include <inttypes.h>
#include <util/delay.h>

#define BAUD 9600                          // baudrate
#define UBRR_VALUE ((F_CPU)/16/(BAUD)-1)   // zgodnie ze wzorem

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
  ADMUX   |= _BV(REFS0); // referencja AVcc
  ADMUX   |= _BV(MUX3) | _BV(MUX2) | _BV(MUX1);	 // Bo mierzymy 1.1V
  
  DIDR0   = _BV(ADC0D); // wyłącz wejście cyfrowe na ADC0 w celu oszczedzania energii
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
  
  // Ustawiam sterowanie dioda led
  DDRD |= _BV(PD2);
  // Zmienna sterujaca zapalaniem (lub nie) diody LED
  // Poniewaz w co drugiej iteracji zapalam diode
  uint8_t iter = 0;
  while(1) {
    if(!iter)
    	PORTD |= _BV(PD2);
    _delay_ms(250);	
    ADCSRA |= _BV(ADSC); // wykonaj konwersję
    while (!(ADCSRA & _BV(ADIF))); // czekaj na wynik
    ADCSRA |= _BV(ADIF); // wyczyść bit ADIF (pisząc 1!)
    
    uint16_t v = ADC; // weź zmierzoną wartość (0..1023)
    
    if(v != 0)
    	printf("VREF: %fV, dioda: %"PRIu8"\r\n", 1.1 * 1024.0/v, !iter);
    else
    	printf("ZERO\r\n");
    // Mierze co sekunde
    //_delay_ms(1000);
    // Zmieniam odpowiednio zmienna odpowiedzialna za wlaczanie leda
    iter++;
    iter%=2;
    // Wylaczam diode
    PORTD &= ~_BV(PD2);
    _delay_ms(250);
  }
}

