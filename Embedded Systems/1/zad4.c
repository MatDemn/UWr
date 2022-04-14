#include <avr/io.h>
#include <util/delay.h>
#include <inttypes.h>

const int8_t DIGITS[10] = {
	0x3F,	//0	ABCDEF- -> -FEDCBA -> 
	0x06,	//1	-BC---- -> ----CB- -> 
	0x5B,	//2	AB-DE-G -> G-RD-BA -> 
	0x4F,	//3	ABCD--G -> G--DCBA -> 
	0x66,	//4	-BC--FG -> GF--CB- -> 
	0x6D,	//5	A-CD-FG -> GF-DC-A -> 
	0x7D,	//6	A-CDEFG -> GFEDC-A -> 
	0x07,	//7	ABC---- -> ----CBA -> 
	0x7F,	//8	ABCDEFG -> GFEDCBA -> 
	0x6F	//9	ABCD-FG -> GF-DCBA -> 
};

#define MAXIM 1

int main()
{
  // program
  UCSR0B &= ~_BV(RXEN0) & ~_BV(TXEN0);		// Bo uzywam pinow tx  rx
  DDRD |= 0xFF;					// Ustawiam wszystkie piny bloku D
  
  
  int8_t iter = 9;
  while(1) {
  	PORTD = ~DIGITS[iter];
  	_delay_ms(1000);
  	iter--;
  	if(iter < 0)
  		iter = 9;
  
  }
}
