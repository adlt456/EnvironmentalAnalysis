#include <Arduino.h>
#include "led.h"

void Task1(void *pvParameters) {
  while(1) {
    Serial.println("Task 1 is running");
    vTaskDelay(1000 / portTICK_PERIOD_MS); // Delay for 1 second
  }
}

void Task2(void *pvParameters) {
  while(1) {
    Serial.println("Task 2 is running. LED toggle");
    toggleLed();
    vTaskDelay(2000 / portTICK_PERIOD_MS); // Delay for 2 seconds
  }
}

void setup() {
  Serial.begin(115200);
  setupToggleLed(5);
  // Create Task 1 and Task 2
  xTaskCreate(Task1, "Task1", 10000, NULL, 1, NULL);
  xTaskCreate(Task2, "Task2", 10000, NULL, 1, NULL);
}

void loop() {
  // Empty loop as tasks are running in the background
}
