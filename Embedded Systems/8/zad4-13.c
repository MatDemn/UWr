/******************************************************************************
 * Header file inclusions.
 ******************************************************************************/

#include "FreeRTOS.h"
#include "task.h"
#include <avr/io.h>
#include <stdio.h>
#include "uart.h"
#include "semphr.h"
#include <avr/interrupt.h>

/******************************************************************************
 * Private macro definitions.
 ******************************************************************************/

SemaphoreHandle_t xADCSemaphore = NULL;

SemaphoreHandle_t xADCResSemaphore = NULL;

SemaphoreHandle_t xPrintSemaphore = NULL;

/******************************************************************************
 * Private function prototypes.
 ******************************************************************************/

static void vPotentiometer(void* pvParameters);

static void vThermistor(void* pvParameters);

static void vPhotoresistor(void* pvParameters);

void adc_init();

/******************************************************************************
 * Public function definitions.
 ******************************************************************************/


// Inicjalizacja ADC
void adc_init() {
	// Vcc as reference, ADC0 as input
	ADMUX = _BV(REFS0);
	// ADC enable, interrupt enabled, 128 prescaller
	ADCSRA = _BV(ADEN) | _BV(ADIE) | _BV(ADPS2) | _BV(ADPS1) | _BV(ADPS0);
	// Digital input disable (power saving) ADC0..5
	DIDR0 = 0x3F;
	// Create Semaphore guarding ADC
	xADCSemaphore = xSemaphoreCreateBinary();
	// Semaphore is 0, so have to give him token
	xSemaphoreGive(xADCSemaphore);
	
	// Create Semaphore guarding result of ADC
	xADCResSemaphore = xSemaphoreCreateBinary();
	// Semaphore is 0, so have to give him token
	xSemaphoreGive(xADCResSemaphore);
	
	// Create Semaphore guarding printf
	xPrintSemaphore = xSemaphoreCreateBinary();
	// Semaphore is 0, so have to give him token
	xSemaphoreGive(xPrintSemaphore);
	
	
}

// Obsluga ADC z synchronizacja
uint16_t readADC(uint8_t mux) {
	
	// Ustawiam odpowiednio ADC do pomiaru
	ADMUX = _BV(REFS0) | mux;
	// Odpal konwersje ADC
	ADCSRA |= _BV(ADSC);
	
	// Jak sie nie uda wziac, to cos jest zle jednak
	if(xSemaphoreTake(xADCResSemaphore, 0) != pdTRUE) {
		printf("ERR readADC %"PRIu8"\r\n", mux);
	}
	//puts("3");
	// Sprobuj pobrac semafor wyniku ADC
	// Uda sie jak przerwanie odda token
	if(xSemaphoreTake(xADCResSemaphore, portMAX_DELAY)) {
		
		// Tutaj funkcja dojdzie jak przerwanie ja odblokuje. Czyli wroci sterowanie do miejsca po wywolaniu readADC.
		//puts("4");
		uint16_t res = ADC;
		xSemaphoreGive(xADCResSemaphore);
		return res;
	}
}

// Przerwanie po wykonaniu konwersji ADC
ISR(ADC_vect) {
	//puts("I");
	// Oddaj semafor, bo po konwersji juz jest
	xSemaphoreGiveFromISR(xADCResSemaphore, NULL);	
	
}

/**************************************************************************//**
 * \fn int main(void)
 *
 * \brief Main function.
 *
 * \return
 ******************************************************************************/

int main(void)
{	
    adc_init();
    sei();
    uart_init();
    stdin = stdout = stderr = &uart_file;
    
    TaskHandle_t xPotentimeter_Handle;
    TaskHandle_t xThermistor_Handle;
    TaskHandle_t xPhotoresistor_Handle;
    
    // Create tasks (they use ADC).
    
    xTaskCreate
        (
         vPhotoresistor,
         "photo",
         configMINIMAL_STACK_SIZE,
         NULL,
         1,
         xPhotoresistor_Handle
        );
    
    xTaskCreate
        (
         vThermistor,
         "therm",
         configMINIMAL_STACK_SIZE,
         NULL,
         1,
         xThermistor_Handle
        );
    
    xTaskCreate
        (
         vPotentiometer,
         "potent",
         configMINIMAL_STACK_SIZE,
         NULL,
         1,
         xPotentimeter_Handle
        );
        
     
        
     
    // Start scheduler.
    vTaskStartScheduler();

    return 0;
}

/**************************************************************************//**
 * \fn static vApplicationIdleHook(void)
 *
 * \brief
 ******************************************************************************/

void vApplicationIdleHook(void)
{

}

/******************************************************************************
 * Private function definitions.
 ******************************************************************************/

static void vPotentiometer(void* pvParameters) {
	uint16_t v = 42;
	vTaskDelay(100 / portTICK_PERIOD_MS);
	while(1) {
		vTaskDelay(1000 / portTICK_PERIOD_MS);
		// Sprobuj wziac semafor, blokuj sie az nie wezmiesz
		if(xSemaphoreTake(xADCSemaphore, portMAX_DELAY)) {
			v = readADC(0);
			printf("------------------\r\n");
			printf("Pot: %"PRIu16"\r\n", v);
		}	
		xSemaphoreGive(xADCSemaphore);
	}
}

static void vThermistor(void* pvParameters) {
	uint16_t v = 42;
	vTaskDelay(2000 / portTICK_PERIOD_MS);
	while(1) {
		vTaskDelay(1000 / portTICK_PERIOD_MS);
		if(xSemaphoreTake(xADCSemaphore, portMAX_DELAY)) {
			v = readADC(1);
			printf("Ter: %"PRIu16"\r\n", v);
		}
		xSemaphoreGive(xADCSemaphore);
		
	}
}

static void vPhotoresistor(void* pvParameters) {
	uint16_t v = 42;
	vTaskDelay(3000 / portTICK_PERIOD_MS);
	while(1) {
		vTaskDelay(1000 / portTICK_PERIOD_MS);
		if(xSemaphoreTake(xADCSemaphore, portMAX_DELAY)) {
			v = readADC(2);
			printf("Fot: %"PRIu16"\r\n", v);
		}
		xSemaphoreGive(xADCSemaphore);
	}
}
