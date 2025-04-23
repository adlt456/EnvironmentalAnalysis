#include "led.h"
#include <Arduino.h>

static int ledPin;
static bool ledState = false;

void setupToggleLed(int pin) {
  ledPin = pin;
  pinMode(ledPin, OUTPUT);
}

void toggleLed() {
  ledState = !ledState;
  digitalWrite(ledPin, ledState ? HIGH : LOW);
}
