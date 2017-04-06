#include <LiquidCrystal_I2C.h>										// Library for lcd i2c communication.
#include <DHT.h>													// Library for tenperature and humidity sensor.
#include "FastLED.h"												// Library for WS2812 led strip
#include <Encoder.h>
#include <Wire.h>

#define ArraySize(x)       (sizeof(x) / sizeof(x[0]))				// Returns size of array
#define NUM_LEDS 70													// Number of leds in led strip
#define DATA_PIN 8													// Data pin for led strip

#define AnalogPinForColors 0
#define StrobePinForColors 6
#define ResetPinForColors 5

#define RotaryEncoderButtonPin 4
#define RotaryEncoderFirstPin 2
#define RotaryEncoderSecondPin 3

bool DEBUG = 0 ;

LiquidCrystal_I2C   lcd(0x27, 2, 1, 0, 4, 5, 6, 7, 3, POSITIVE);	// Initializes lcd object for i2c communication.
DHT dht(7, DHT22);													// Initializes dht object for temperature checking.
CRGB leds[NUM_LEDS];
Encoder myEnc(RotaryEncoderFirstPin, RotaryEncoderSecondPin);		// D2, D3 pins for encoder

long oldPosition = -999;											// position of rotary encoder

void setup()
{			
	Serial.begin(115200);												// Starts serial communication at baud rate 9600.
	lcd.begin(16, 2);												// Starts 16x2 lcd screen 	
	FastLED.addLeds<WS2812, DATA_PIN, GRB>(leds, NUM_LEDS);
	pinMode(AnalogPinForColors, INPUT);
	pinMode(StrobePinForColors, OUTPUT);
	pinMode(ResetPinForColors, OUTPUT);

	digitalWrite(ResetPinForColors, LOW);
	digitalWrite(StrobePinForColors, HIGH);

	oldPosition = myEnc.read();
}


int waitTime[] = {2500, 10000};										// Number tells how long specific lcd mode will be displayed.
bool shiftTurn[] = {true, false};									// True tells which lcd mode will be displayed next. DO NOT CHANGE IT.
int nextWait = waitTime[0];											// Time for timer. First time is set to waitTime[0]
int lcdMode = 0;													// Lcd mode to display different info.
long _time = 0;														// Time used for timer.
void loop()
{	
	CheckSCom();													// Checks if any serial communication is present.
	GetNewTemperatureReadings();									// Checks current temperature in the room.
	CheckEncoder();
	colors();
	
	if(_time + nextWait < millis())									// Timer that ticks every 'nextWait' miliseconds.
	{		
		_time = millis();											// Once timer ticks. we reset _time in order to timer tick later.

		for(byte i = 0; i < ArraySize(waitTime); i++)				// Loop through all lcd modes
		{
			if(shiftTurn[i] == true)								// Find lcd mode to display
			{
				lcdMode = i;										// Set lcd mode to turn which was true
				shiftTurn[i] = false;								// Then set that turn to false

				if(i == ArraySize(waitTime)-1)						// And set next item in array to be next lcd mode.
				{
					shiftTurn[0] = true;					
					nextWait = waitTime[0];
				}					
				else{
					shiftTurn[i+1] = true;
					nextWait = waitTime[i+1];
				}
				break;
			}
		}
	} 	
	
	IdkHowToNametThis(lcdMode);										// Update lcd with current mode.
}

long clickMillis=0;
boolean pressed = false;											// Tikrina ar paspaudia rotrary encoder buttona ir jei taip padaro true
int clickCounter = 0;
void CheckEncoder()
{
	if (!(digitalRead(RotaryEncoderButtonPin))) {					// Readina Rotary encoderio button paspaudima
		if (pressed == false){										//Tikrina ar jau buvo isvietes funkcija, nes pausaudimas loope yra uzfiksuojmas kiekviena karta			
			if(clickMillis+350>millis())
			{				
				clickCounter++;				
				Click(clickCounter+1);
			}
			else
			{
				clickCounter=0;
				Click(1);			
			}

			clickMillis=millis();
		}
		pressed = true;
		
	}
	else {
		pressed = false;
	}

	long newPosition = myEnc.read();								// Tikrina rotary encoderio posicija
	if (newPosition % 4 == 0 && newPosition != oldPosition) {       // Kadangi rotary encoderis per 1 palinkima padideja 4 kartus o man reikia tik 1,
		if (newPosition > oldPosition)                              // Tai as ziuriu ar yra liekana dalyjant is 4, jei ne tai pasisuko 1 karta
			left();
		else
			right();
		oldPosition = newPosition;
	}
}

void Click(int i)
{


	Serial.print("!cl"); // Play or pause music
	Serial.print(i);
	Serial.print("\n"); 
}

void right()
{
	Serial.println("!+");
}

void left()
{
	Serial.println("!-");
}


uint16_t gHue = 0;
uint8_t  gHueDelta = 1;

int colorMode = 0;
bool colorsEnabled = true;
int spectrumValue[7];												// Array kuris isfiltruja daznius. Basai 0,1 mid 3,4 high 5,6,7
int filter = 80;
void colors() {
	if(!colorsEnabled) return;
	digitalWrite(ResetPinForColors, HIGH);
	digitalWrite(ResetPinForColors, LOW);
	for (int i = 0; i<7; i++) {
		digitalWrite(StrobePinForColors, LOW);
		delay(1);
		CheckEncoder();
		delay(1);
		CheckEncoder();
		delay(1);
		CheckEncoder();
		spectrumValue[i] = analogRead(AnalogPinForColors);
		spectrumValue[i] = constrain(spectrumValue[i], filter, 1023);
		spectrumValue[i] = map(spectrumValue[i], filter, 1023, 0, 255);
		if(DEBUG){
			if(spectrumValue[i] < 10){
				Serial.print("  "); Serial.print(spectrumValue[i]); Serial.print(" ");
			} else if(spectrumValue[i] < 100){
				Serial.print(" "); Serial.print(spectrumValue[i]); Serial.print(" ");
			} else{
				Serial.print(spectrumValue[i]); Serial.print(" ");
			}
		}
		digitalWrite(StrobePinForColors, HIGH);
	}
	if(DEBUG){
		Serial.println(); 
	}


	if(colorMode==0)
	{
		for (int i = 0; i < 70; i++) {
			leds[i].setHSV( 128, 255, 0);
		}

		for (int i = 0; i < 7; i++) {
			for(int j = 0; j < 10; j++)
			{
				int color = 0;
				if(i == 0) color = 98;
				if(i == 1) color = 160;
				if(i == 2) color = 32;
				if(i == 3) color = 192;
				if(i == 4) color = 128;
				if(i == 5) color = 244;
				if(i == 6) color = 98;

				if(spectrumValue[i] > j*25)
					leds[i*10+j].setHSV( color, 255, 255);
			}
		}
	} else if (colorMode==1)
	{
  for (int i = 0; i < 10; i++) {
      leds[i].setHSV(64, 255, spectrumValue[0]);
    }
		for (int i = 0; i < 10; i++) {
			leds[i + 10].setHSV(98, 255, spectrumValue[1]);
		}
		for (int i = 0; i < 10; i++) {
			leds[i + 20].setHSV(160, 255, spectrumValue[2]);
		}
		for (int i = 0; i < 10; i++) {
			leds[i + 30].setHSV(32, 255, spectrumValue[3]);
		}
		for (int i = 0; i < 10; i++) {
			leds[i + 40].setHSV(192, 255, spectrumValue[4]);
		}
		for (int i = 0; i < 10; i++) {
			leds[i + 50].setHSV(128, 255, spectrumValue[5]);
		}
		for (int i = 0; i < 10; i++) {
			leds[i + 60].setHSV(244, 255, spectrumValue[6]);
		}
	} else if (colorMode==2){
    gHue += gHueDelta; // compute new hue

       
    for (int i = 0; i < 70; i++) {
      leds[i].setHSV(gHue, 255, 150);
    }
	  
 }


	
	

	/*s//if(spectrumValue[3] < 25 && spectrumValue[3] > 5)
		leds[0].setHSV( 128, 255, 255);
	//if(spectrumValue[3] > 25 )
		leds[1].setHSV( 128, 255, 255);
	//if(spectrumValue[3] > 50 )
		leds[2].setHSV( 128, 255, 255);
	//if(spectrumValue[3] > 75 )
		leds[3].setHSV( 128, 255, 255);
	//if(spectrumValue[3] > 100 )
		leds[4].setHSV( 128, 255, 255);
	//if(spectrumValue[3] > 125 )
		leds[5].setHSV( 128, 255, 255);
	//if(spectrumValue[3] > 150 )
		leds[6].setHSV( 128, 255, 255);
	//if(spectrumValue[3] > 175 )
		leds[7].setHSV( 128, 255, 255);
	//if(spectrumValue[3] > 120 )
		leds[8].setHSV( 128, 255, 255);*/


	
	FastLED.show();
}


long messageTime= 0;
String songTitleHold = "";											// Chached song title used for comparison.
float temperatureHold = 0;											// Cached temperature
float humidityHold = 0;												// Cached humidity
void IdkHowToNametThis(byte i)
{	
  if(messageTime > millis()) return;
	switch (i) {
		case 0:
			ShowSongs();											// Checks if any song is playing and if it is, then it updates lcd			
			temperatureHold = 0;									// If we dont set this to 0, then lcd wont update because of refresh protection
			humidityHold = 0;										// If we dont set this to 0, then lcd wont update because of refresh protection
		break;
		case 1:
			ShowTemperature();										// Shows current temperature		
			songTitleHold="";										// If we dont set this to 0, then lcd wont update because of refresh protection
		break;
		default: 

		break;
	}
}

void showMessage(String message, int ttime){
  messageTime= ttime + millis();  
   songTitleHold = "";                      // Chached song title used for comparison.
 temperatureHold = 0;                      // Cached temperature
 humidityHold = 0;
  lcd.clear();
  lcd.print(message);
}

float temperature = 0;												// Current temperature from sensor
float humidity = 0;													// Current humidity from sensor
long DHTdelay = 0;													// Delay used in timer to refresh sensor
bool showTemperature = false;										// Should i show you temperature?
void GetNewTemperatureReadings()									// Void used to refresh temeprature numbers
{
	if(DHTdelay + 10000 < millis()) {								// Timer that ticks every 10 seconds and refreshes temperature
		DHTdelay = millis();										// Delay is set to millis in order for it to tick in future
		float h = dht.readHumidity();								// Reads humidity float
		float t = dht.readTemperature();							// reads temeprature float 
		if (isnan(h) || isnan(t)) {									// If readings are invalid, then
			Serial.println("Failed to read from DHT sensor!");		// Print error message
			return;													// And quit the void
		}
		temperature = round(t);										// But if they are valid, then round them up and set
		humidity = round(h);										// Floats to them.
		showTemperature = true;										// And allow temeprature to be shown.
	}
}

void ShowTemperature()
{
	if (showTemperature) {											// If temperature can be showed.
		if (temperature!=temperatureHold||humidity!=humidityHold) { // Compare temp with cached temp, so I dont have to refresh lcd every time
			lcd.clear();
			humidityHold = humidity;
			temperatureHold = temperature;							
			lcd.setCursor(0, 0);									// First line of lcd
			lcd.print("Temperatura: ");
			lcd.print(temperature);									// Print temperature to first line
			lcd.setCursor(15, 0);									// Place cursor at 15th block so I can print 'C' letter
			lcd.print("C ");

			lcd.setCursor(0, 1);									// Second line of lcd
			lcd.print("Dregnumas:   ");
			lcd.print(humidity);									// Print humidity to second line	
			lcd.setCursor(15, 1);									// Place cursor at 15th block so I can print '%' sign
			lcd.write('%');											
		}
	}
}

String readString;													// Buffer string for serial communication.
void CheckSCom()
{		
	while (Serial.available()) {									// While serial text is available.	
		char c = Serial.read();										// We read each char in serial buffer.
		if (c == '\n') {											// And if char is new line, then that means its end of command.
			SComCommandRecieved(readString);						// So we call that command			
			readString = "";										// And reset string buffer
		} 
		else
			readString += c;										// And if its not newline, then we add that char to buffer.
	}
}


String songTitle;													// Current song title
String songArtist;													// Current song artist
bool songPlaying = true;											// Is song playing now?
void ShowSongs()													// Void that prints song name and title to lcd
{
	if (songPlaying) {												// If song is currently playing, then show it
		if (songTitleHold != songTitle) {							// Compare title with cached title, so I dont have to refresh lcd every time
			lcd.clear();
			songTitleHold = songTitle;								// Set title cache to current song if its new song.
			lcd.setCursor(0, 0);									// First line of lcd
			lcd.print(songTitle);									// Print title to first line
			lcd.setCursor(0, 1);									// Second line of lcd
			lcd.print(songArtist);									// Print artist to second line
		}
	}
}



void SComCommandRecieved(String text)								// Fired when Serial Communication commadn is recieved.
{		
	if (text.substring(0, 5) == "title") {							// If title command is recieved
		songTitle = text.substring(5, text.length());				// Then parse string so I get song name.
		if (songTitle == "")										// if song name is nothing, then it means no song is playing
			songPlaying = false;									// So we set songPlaying to false
		else														// but if it is something
			songPlaying = true;										// then we set songPlaying to true
	}
	if (text.substring(0, 6) == "artist") {							// If artist command is recieved
		songArtist = text.substring(6, text.length());				// Then parse string so I get song artist.
	} else {
	   if (text.substring(0, 1) == "a" ) {             // If artist command is recieved
    int xd = text.substring(1, text.length()).toInt();
      Wire.begin(); // join i2c bus (address optional for master)
      Wire.beginTransmission(8); // transmit to device #8
      Wire.write("a");        // sends five bytes
      Wire.write(xd);              // sends one byte
      Wire.endTransmission();    // stop transmitting     
    }
    if (text.substring(0, 1) == "r") {              
      int xd = text.substring(1, text.length()).toInt();
        Wire.begin(); // join i2c bus (address optional for master)
        Wire.beginTransmission(8); // transmit to device #8
        Wire.write("r");        // sends five bytes
        Wire.write(xd);              // sends one byte
        Wire.endTransmission();    // stop transmitting   
    }  

      if (text.substring(0, 1) == "v" ) {             // If artist command is recieved
      int xd = text.substring(1, text.length()).toInt();
      showMessage("Volume: " +  String(xd) , 2000);  
    }
 
	}
	if (text.substring(0, 9) == "colormode") {							
		colorMode = text.substring(9, text.length()).toInt();			
	}

	if(text == "colorson")
		colorsEnabled=true;
	if(text == "colorsoff")
	{
		colorsEnabled=false;
		for (int i = 0; i < 70; i++) {
			leds[i].setHSV( 128, 255, 0);
		}
		FastLED.show();
	}
	if(text == "debugon")
		DEBUG=true;
	if(text == "debugoff")
		DEBUG=false;
 
  
		
	
}
