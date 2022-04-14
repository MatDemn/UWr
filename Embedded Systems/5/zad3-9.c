#include <avr/io.h>
#include <util/delay.h>
#include <avr/interrupt.h>
#include <stdio.h>
#include <avr/sleep.h>

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
  ADMUX   = _BV(REFS0) | _BV(MUX3) | _BV(MUX2) | _BV(MUX1); // referencja AVcc, mierzenie 1.1V
  ADCSRA  = _BV(ADPS2) | _BV(ADPS1) | _BV(ADPS0) | _BV(ADIE); // preskaler 128, przerwania wlaczone
  ADCSRA |= _BV(ADEN); // włącz ADC
}

volatile uint32_t measuredVoltage = 0;

ISR(ADC_vect) {
	// Nie potrzebuje obslugiwac w jakis posob przerwania ADC, wiec tutaj pusto
}

FILE uart_file;

volatile int32_t interruptSum = 0;
volatile int32_t activeWaitSum = 0;

// Wariancja: [ (X1-XS)^2 + (X2-XS)^2 + ... + (Xn-XS)^2 ]/n
// Gdzie: X1, X2, ..., Xn - pomiary; XS - srednia dla pomiarow (tutaj to bedzie 1.1V = 1100mV); n - liczba pomiarow

int main() {
  uint8_t PROBY = 250;
  // zainicjalizuj UART
  uart_init();
  // skonfiguruj strumienie wejścia/wyjścia
  fdev_setup_stream(&uart_file, uart_transmit, uart_receive, _FDEV_SETUP_RW);
  stdin = stdout = stderr = &uart_file;
  
  adc_init();
  sei();
  
  set_sleep_mode(SLEEP_MODE_ADC);
  // Liczenie z usypianiem i przerwaniami
  _delay_ms(1000);
  // printf("Wyniki dla NOISE REDUCTION:\r\n");
  _delay_ms(10);
  for(uint8_t i = 0; i<PROBY; i++) {
  	sleep_mode();		// Uspij procesor do czasu zakonczenia konwersji
  	
  	measuredVoltage = ADC;
  	measuredVoltage *= 5000;
  	measuredVoltage /= 1024;
  	
  	int16_t temp = measuredVoltage - 1100;
  	interruptSum += temp*temp;
  	
  	//printf("%"PRIu32"mV\r\n", measuredVoltage);	// Wypisz zmierzona wartosc po konwersji
  	_delay_ms(10);
  } 
  // printf("Wyniki dla aktywnego czekania:\r\n");
  _delay_ms(10);
  // Czyszcze przerwania
  ADCSRA  &= ~_BV(ADIE); // przerwania wylaczone
  // Liczenie bez usypiania, krecac sie w petli
  for(uint8_t i = 0; i<PROBY; i++) {
  	ADCSRA |= _BV(ADSC);
  	while(!(ADCSRA & _BV(ADIF)));
  	
  	measuredVoltage = ADC;
  	measuredVoltage *= 5000;
  	measuredVoltage /= 1024;
  	
  	int16_t temp = measuredVoltage - 1100;
  	activeWaitSum += temp*temp;
  	
  	//printf("%"PRIu32"mV\r\n", measuredVoltage);	// Wypisz zmierzona wartosc po konwersji
  } 
  
  printf("Wariancja dla NOISE REDUCTION: %"PRIu32"\r\n", interruptSum/PROBY);
  
  printf("Wariancja dla aktywnego czekania: %"PRIu32"\r\n", activeWaitSum/PROBY);
  
  
}
