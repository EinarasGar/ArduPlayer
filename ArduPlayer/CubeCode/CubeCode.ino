#include <Wire.h>

int Data_pin = 11;
int STCP_pin = 8;
int SHCP_pin = 12;

boolean registers[32];
boolean Floor1[25];
boolean Floor2[25];
boolean Floor3[25];
boolean Floor4[25];
boolean Floor5[25];

boolean leds[125];

void setup() {
  pinMode(Data_pin,OUTPUT);     // no problem here
  pinMode(STCP_pin,OUTPUT);
  pinMode(SHCP_pin,OUTPUT);
  writereg();  
   Wire.begin(8);                // join i2c bus with address #8
  Wire.onReceive(receiveEvent); 
  Serial.begin(9600);

}
void writereg()
{
  digitalWrite(STCP_pin, LOW);
  for (int i = 0; i<32; i++)     // for shifters
  {
    digitalWrite(SHCP_pin, LOW);
    digitalWrite(Data_pin, registers[i] );
    digitalWrite(SHCP_pin, HIGH);
  }
  digitalWrite(STCP_pin, HIGH);
}


void loop() {   
CheckSCom();    

  LightLeds();
    
    
}

String readString;                          // Buffer string for serial communication.
void CheckSCom()
{   
  while (Serial.available()) {                  // While serial text is available.  
    char c = Serial.read();                   // We read each char in serial buffer.
    if (c == '\n') {                      // And if char is new line, then that means its end of command.
      SComCommandRecieved(readString);            // So we call that command      
      readString = "";                    // And reset string buffer
    } 
    else
      readString += c;                    // And if its not newline, then we add that char to buffer.
  }
}

void SComCommandRecieved(String text)                // Fired when Serial Communication commadn is recieved.
{  
  if (text.substring(0, 1) == "a") {             // If artist command is recieved
    int xd = text.substring(1, text.length()).toInt();
   leds[xd] = HIGH;   
  }
  if (text.substring(0, 1) == "r") {              
    int xd = text.substring(1, text.length()).toInt();
   leds[xd] = LOW;   
  }
  
}

int buffer[125];
int readAnimation = false;
int buffer_counter = 0;
void receiveEvent(int howMany) {


	/*while (0 < Wire.available()) {
		byte b = Wire.read();
		leds[b] = HIGH;
		if (b == 201)
		{
			for (int i = 0; i < 125; ++i)
			{
				leds[i] = LOW;
			}
			//  leds[0] = HIGH;
			//	setLeds();
			//   buffer_counter = 0;
			//	return;
		}
		//	leds[b] = HIGH;
		//	buffer[buffer_counter] = b;
		//	buffer_counter++;*/
		
	while (0 < Wire.available()) {
		byte b = Wire.read();

		if (readAnimation)
		{
			if (b != 200)
				buffer[buffer_counter] = b;
			buffer_counter++;
		}
		if (b == 200)
		{
			readAnimation = true;
		}
		if (b == 201)
		{
			readAnimation = false;
			setFrame();
			buffer_counter = 0;
			
		}
	}
}

void setFrame()
{
    for (int i = 0; i < 125; ++i)
  {
    leds[i] = LOW;
  }
	for (int i = 0; i < buffer_counter - 1; ++i)
	{
		leds[buffer[i]] = HIGH;
	}
}

/*//while (1 < Wire.available()) { // loop through all but the last
    char c = Wire.read(); // receive byte as a character
   // Serial.print(c);         // print the character
 // }
  int x = Wire.read();    // receive byte as an integer
 // Serial.println(x);         // print the integer

 if (c == 'a') {             // If artist command is recieved
	 leds[x] = HIGH;
   
  }
  if (c == 'r') {              
   
   leds[x] = LOW;   
  }*/

//}

/*void setLeds()
{
    for (int i = 0; i < 125; ++i)
  {
    leds[i] = LOW;
  }
	for (int j = 0; j < buffer_counter; ++j)
	{
		leds[buffer[j]] = HIGH;
	}
}*/

void SelectLed(byte number){
  leds[125] = HIGH;
}

void DeselectLed(byte number){
  leds[125] = LOW;
}

void ClearRegisters(){
  for(int i = 0 ; i < 32 ; i ++ ){
    registers[i]  = LOW;
  }  
}

void LightLeds(){
 
  //PENKTAS AUKSTAS
  ClearRegisters();
  for(int i = 0 ; i < 25; i++){
      if(leds[i] == HIGH)
        registers[SelectLedz(i)]  = HIGH;        
  }
  registers[6] = HIGH;
  writereg(); 

  //KETVIRTAS AUKSTAS
  ClearRegisters();
  for(int i = 25 ; i < 50; i++){
      if(leds[i] == HIGH)
        registers[SelectLedz(i-25)]  = HIGH;        
  }
  registers[5] = HIGH;
  writereg(); 

   //TRECIAS AUKSTAS
  ClearRegisters();
  for(int i = 50 ; i < 75; i++){
      if(leds[i] == HIGH)
        registers[SelectLedz(i-50)]  = HIGH;        
  }
  registers[4] = HIGH;
  writereg(); 

  //ANTRAS AUKSTAS
  ClearRegisters();
  for(int i = 75 ; i < 100; i++){
      if(leds[i] == HIGH)
        registers[SelectLedz(i-75)]  = HIGH;        
  }
  registers[3] = HIGH;
  writereg(); 

  //PIRMAS AUKSTAS
  ClearRegisters();
  for(int i = 100 ; i < 125; i++){
      if(leds[i] == HIGH)
        registers[SelectLedz(i-100)]  = HIGH;        
  }
  registers[2] = HIGH;
  writereg(); 
   
}

int SelectLedz(byte number){
if (number ==24)  return 24;
if (number ==23)  return 25;
if (number ==22)  return 26;
if (number ==21)  return 27;
if (number ==20)  return 28;
if (number ==19)  return 29;
if (number ==18)  return 30;
if (number ==17)  return 31;
if (number ==16)  return 1;
if (number ==15) return 17;
if (number == 14) return 18;
if (number == 13) return 19;
if (number == 12) return 20;
if (number == 11) return 21;
if (number == 10) return 22;
if (number == 9) return 23;
if (number == 8) return 8;
if (number == 7) return 9;
if (number == 6) return 10;
if (number == 5) return 11;
if (number == 4) return 12;
if (number == 3) return 13;
if (number == 2) return 14;
if (number == 1) return 15;
if (number == 0) return 7;
return 24;
}

// 31 - 8
// 30 - 7
// 29 - 6
// 28 - 5
// 27 - 4
// 26 - 3
// 25 - 2
// 24 - 1
// 23 - 16
// 22 - 15
// 21 - 14
// 20 - 13
// 19 - 12
// 18 - 11
// 17 - 10
// 16 - 9
// 15 - 24
// 14 - 23
// 13 - 22
// 12 - 21
// 11 - 20
// 10 - 19
// 9  - 18
// 8  - 17
// 7  - 25
// 6  - 30 // 5 aukstas
// 5  - 29 // 4 aukstas
// 4  - 28 // 3 aukstas
// 3  - 27 // 2 aukstas
// 2  - 26 // 1 aukstas

















