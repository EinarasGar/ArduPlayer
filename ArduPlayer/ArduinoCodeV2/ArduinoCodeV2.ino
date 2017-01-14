#include <LiquidCrystal_I2C.h>										// Library for lcd i2c communication.
#include <DHT.h>													// Library for tenperature and humidity sensor.

#define ArraySize(x)       (sizeof(x) / sizeof(x[0]))				// Returns size of array

LiquidCrystal_I2C   lcd(0x27, 2, 1, 0, 4, 5, 6, 7, 3, POSITIVE);	// Initializes lcd object for i2c communication.
DHT dht(7, DHT22);													// Initializes dht object for temperature checking.

void setup()
{			
	Serial.begin(9600);												// Starts serial communication at baud rate 9600.
	lcd.begin(16, 2);												// Starts 16x2 lcd screen 	
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


String songTitleHold = "";											// Chached song title used for comparison.
float temperatureHold = 0;											// Cached temperature
float humidityHold = 0;												// Cached humidity
void IdkHowToNametThis(byte i)
{	
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
	}
}