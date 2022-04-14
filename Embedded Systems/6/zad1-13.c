#include <avr/io.h>
#include <avr/interrupt.h>
#include <avr/sleep.h>

#define BAUD 9600                          // baudrate
#define UBRR_VALUE ((F_CPU)/16/(BAUD)-1)   // zgodnie ze wzorem

void uart_init()
{
  // ustaw baudrate
  UBRR0 = UBRR_VALUE;
  // wyczyść rejestr UCSR0A
  UCSR0A = 0;
  // włącz odbiornik i nadajnik, wlacz przerwania dla RecieveComplete
  UCSR0B = _BV(RXCIE0) | _BV(RXEN0) | _BV(TXEN0);
  // ustaw format 8n1
  UCSR0C = _BV(UCSZ00) | _BV(UCSZ01);
}

ISR(USART_RX_vect) {
	uint8_t recData;
	recData = UDR0;
	UDR0 = recData;
	if(recData == '\r') {
		UDR0 = '\n';
	}
}
int main() {
	uart_init();
	sei();
	set_sleep_mode(SLEEP_MODE_IDLE);
	while(1) {
		sleep_mode();
	}
}
