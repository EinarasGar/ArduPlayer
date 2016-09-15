#include <Encoder.h>
#include <Wire.h>
#include <LiquidCrystal_I2C.h>
#include <DHT.h>
//asddd
#define ArraySize(x)       (sizeof(x) / sizeof(x[0])) // Returns size of array
#define DHTPIN 7     // what pin we're connected to
#define DHTTYPE DHT22   // DHT 22  (AM2302)

boolean pressed = false; // Tikrina ar paspaudia rotrary encoder buttona ir jei taip padaro true
bool ColorOrganOn = true;
int analogPin=0; // Analog pinas Msgeq7
int strobePin=6; // Stobe pinas Msgeq7
int resetPin=5; // Reset pinas Msgeq7
int ledred=9; // pinas kuris eina I melyno ledo mosfet GATE pina ||| ->  (|)||
int ledblue=10;
int ledgreen=11;
int spectrumValue[7]; // Array kuris isfiltruja daznius. Basai 0,1 mid 3,4 high 5,6,7
int filter=80;
byte ButtonPin = 4; // rotary encoderio button pinas
long oldPosition = -999; // position of rotary encoder
double redBright = 1;
double blueBright = 1;
double reenBright = 1;
boolean menuOpened = false; // Tikrina ar Menu yra atidarytas
String menuStrings[] = {"      MENU", "Sensitivity", "Reset", "GameOfThrones", "StarWars", "Darude", "Bernardas"}; // Pasirinkimai nemu liste, pridedant viena stringa pailgeja meniu be kitu pokyciu.
String menuSection1Strings[] = {"  SENSITIVITY", "Red", "Green", "Blue", "Turn On/Off", "Back"}; // Pasirinkimai nemu liste, pridedant viena stringa pailgeja meniu be kitu pokyciu.
int menuLine = 0; // Skaicius kurioje eilute menu as esu
int menuSection = 0;
long menuOpenedThreshold =0;
long DHTdelay = 0;
float temperature = 0;
float humidity = 0;
bool ShowTemp = false;
bool ShowSong = true;
long lcdTimeThreshold;
unsigned int switchingTime = 5000;
String songArtist="";
String songTitle="";
String songTitleHold = ""; // I dont need artist because its just for checking if it should update song

LiquidCrystal_I2C   lcd(0x27, 16, 2); // A4 - SDA          A5 - SCL
Encoder myEnc(2, 3);  // D2, D3 pinai rotary encoderio
DHT dht(DHTPIN, DHTTYPE);


void setup() {
  pinMode(analogPin, INPUT);
  pinMode(strobePin, OUTPUT);
  pinMode(resetPin, OUTPUT);
  pinMode(ledred, OUTPUT);
  pinMode(ledblue, OUTPUT);
  pinMode(ledgreen, OUTPUT);
  digitalWrite(resetPin, LOW);
  digitalWrite(strobePin, HIGH);
  digitalWrite(12, HIGH); // Arduino reset funkcijos pinas, Kol jis high tol arduino veikia
  delay(200);
  pinMode(12, OUTPUT);
  lcd.begin();
  Serial.begin(9600);
  Serial.println("Basic Encoder Test:");
  oldPosition = myEnc.read(); // pakeicia inta i rotary encoderio dabartine posicija
  pinMode(ButtonPin,INPUT); // button inputas encoderio
  //pinMode(9, OUTPUT); // naudojau speakeriui bet uzimtas
}

void loop() {

  String readString;
  while(Serial.available()) {
    delay(3);
    char c = Serial.read();
    if(c=='\n'){
      ParseSerialFunction(readString);
      //  Serial.println(readString);
    }
    readString += c;

  }



  if(ColorOrganOn) // Jei color organas ijungtas, tai paleisti funkcija kuri kontroliuoja sviesas
  colors();


  if (!(digitalRead(ButtonPin))){  // Readina Rotary encoderio button paspaudima
    if(pressed == false) //Tikrina ar jau buvo isvietes funkcija, nes pausaudimas loope yra uzfiksuojmas kiekviena karta
    Click();
    pressed = true;
  } else {
    pressed = false;
  }

  long newPosition = myEnc.read();  // Tikrina rotary encoderio posicija
  if (newPosition % 4 == 0 && newPosition != oldPosition) {       // Kadangi rotary encoderis per 1 palinkima padideja 4 kartus o man reikia tik 1,
    if(newPosition > oldPosition)                                 // Tai as ziuriu ar yra liekana dalyjant is 4, jei ne tai pasisuko 1 karta
    left();
    else
    right();
    oldPosition = newPosition;
  }


  if(lcdTimeThreshold + switchingTime < millis()){
    lcd.clear();
    if(ShowSong){
      ShowSong = false;
      ShowTemp = true;
      lcd.setCursor (0,0);      //this whole lcd code was used because
      lcd.print("Temperatura: "); // having lcd print text on every loop
      lcd.setCursor (15,0); // caused loop to slow down a lot
      lcd.print("C "); // I learnt this the hard way :(
      lcd.setCursor (0,1);
      lcd.print("Dregnumas:   ");
      lcd.setCursor (15,1);
      lcd.write('%');
      lcd.setCursor (13,0);
      lcd.print(round(temperature));
      lcd.setCursor (13,1);
      lcd.print(round(humidity));
    } else {
      ShowSong = true;
      ShowTemp = false;
      lcd.setCursor (0,0);
      lcd.print(songTitle);
      lcd.setCursor (0,1);
      lcd.print(songArtist);
    }
    lcdTimeThreshold = millis();
  }

  if(ShowSong){ // Kai siunciu change, siusti Autoriu ir tik tada pavadinima
    if(songTitleHold != songTitle){
      lcd.clear();
      songTitleHold = songTitle;
      lcd.setCursor (0,0);
      lcd.print(songTitle);
      lcd.setCursor (0,1);
      lcd.print(songArtist);
    }
  }

  if(ShowTemp){
    if(!menuOpened && DHTdelay+10000 < millis()){
      DHTdelay = millis();
      float h = dht.readHumidity();
      float t = dht.readTemperature();
      if (isnan(h) || isnan(t)) {
        Serial.println("Failed to read from DHT sensor!");
        return;
      }
      temperature = round(t);
      humidity = round(h);
      Serial.println("!");
      lcd.setCursor (13,0);
      lcd.print(round(temperature));
      lcd.setCursor (13,1);
      lcd.print(round(humidity));
    }
  }


  if(menuOpenedThreshold+10000 < millis()){
    menuClose();
  }

}

void RESTART(){
  lcd.clear();
  lcd.setCursor(0,0);
  lcd.print("   RESTARTING");
  digitalWrite(12, LOW);
}

void saveBright(double a){ // Ifai tikrina kuria spalva noriu redaguoti, o parametras nusako value tu spalvu
  if(menuSection == 2)
  redBright = a;
  if(menuSection == 4)
  blueBright = a;
  if(menuSection == 3)
  reenBright = a;
}

void ParseSerialFunction(String text){
  if(text == "colorsoff"){
    ColorOrganOn = false;
    analogWrite(ledred,0);
    analogWrite(ledgreen,0);
    analogWrite(ledblue,0);
  }
  if(text == "colorson"){
    ColorOrganOn = true;
  }
  if(text == "hey!"){
    Serial.println("Hello!");
  }
  if(text == "giveInfo"){
    Serial.println("{" + String(ColorOrganOn) + "/" + temperature + "/"  +  humidity + "}");
  }
  if (text.substring(0,3) == "red") {
    redBright = text.substring(3,text.length()).toFloat();
  }
  if (text.substring(0,3) == "grn") {//Turi buti blue, bet po kolkas green nes sumaisiau connectionus.
    blueBright = text.substring(3,text.length()).toFloat();
  }
  if (text.substring(0,3) == "blu") {
    reenBright = text.substring(3,text.length()).toFloat();
  }
  if(text.substring(0,5) == "title"){
  //  if(ShowSong)
  //  lcd.clear();
      songTitle=text.substring(5,text.length());
  }
  if(text.substring(0,6) == "artist"){
  //  if(ShowSong)
  //  lcd.clear();
      songArtist=text.substring(6,text.length());
  }

}

void colors(){
  digitalWrite(resetPin, HIGH);
  digitalWrite(resetPin, LOW);
  for (int i=0;i<7;i++){
    digitalWrite(strobePin, LOW);
    delay(3);
    spectrumValue[i]=analogRead(analogPin);
    spectrumValue[i]=constrain(spectrumValue[i], filter, 1023);
    spectrumValue[i]=map(spectrumValue[i], filter,1023,0,255);
    Serial.print(spectrumValue[i]); Serial.print(" ");
    digitalWrite(strobePin, HIGH);
  }
  Serial.println();
  if(spectrumValue[1]*redBright < 255)
  analogWrite(ledred,spectrumValue[1]*redBright);
  else
  analogWrite(ledred,255);

  if(spectrumValue[3]*blueBright < 255)
  analogWrite(ledblue,spectrumValue[3]*blueBright);
  else
  analogWrite(ledblue,255);

  if(spectrumValue[5]*reenBright < 255)
  analogWrite(ledgreen,spectrumValue[5]*reenBright);
  else
  analogWrite(ledgreen,255);
}



void openMenu(){
  updateMenu();
}

void MenuGoUp(){
  if(menuLine > 0 )
  menuLine--;
  updateMenu()  ;
}

void MenuGoDown(){
  switch (menuSection) {
    case 0:
    if(menuLine <  ArraySize(menuStrings) - 1 )
    menuLine++;
    break;
    case 1:
    if(menuLine <  ArraySize(menuSection1Strings) - 1 )
    menuLine++;
    break;
    case 2:
    case 3:
    case 4:
    if(menuLine < 40 )
    menuLine++;
    break;
    default:
    // if nothing else matches, do the default
    // default is optional
    break;
  }
  updateMenu();
}

void menuClick(){
  if(!menuOpened){
    openMenu();
    menuOpened=true;
  } else {
    /////////////////////////////////////////////////////////////////////  RED/GREEN/BLUE value pakeitimo clickai
    if(menuSection == 2 || menuSection == 3 || menuSection == 4){
      menuSection = 1;
      menuLine=0;
      updateMenu();
    }
    /////////////////////////////////////////////////////////////////////    PAGRINDINIO MENU CLICKAI
    if(menuSection == 0){
      switch (menuLine) {
        case 0: // Popovas
        menuClose();
        break;
        case 1: // Popovas
        menuSection = 1;
        menuLine=0;
        updateMenu();
        break;
        case 2: // RESTART
        RESTART();
        break;
        default:
        // if nothing else matches, do the default
        // default is optional
        break;
      }
    }
    /////////////////////////////////////////////////////////////////////  Sensitivity menu clickai
    if(menuSection == 1){
      switch (menuLine) {
        case 1: // RED
        menuSection = 2;
        menuLine=20;
        updateMenu();
        break;
        case 2: // GREEN
        menuSection = 3;
        menuLine=20;
        updateMenu();
        break;
        case 3: // BLUE
        menuSection = 4;
        menuLine=20;
        updateMenu();
        break;
        case 4: // ONOFF
        menuSection = 1;
        menuLine=4;
        if(ColorOrganOn)
        ColorOrganOn=false;
        else
        ColorOrganOn=true;
        analogWrite(ledred,0);
        analogWrite(ledgreen,0);
        analogWrite(ledblue,0);
        updateMenu();
        Serial.println("!");
        break;
        case 5: // BACK
        menuSection = 0;
        menuLine=0;
        updateMenu();
        break;
        default:
        // if nothing else matches, do the default
        // default is optional
        break;
      }
    }
    /////////////////////////////////////////////////////////////////////
  }
}



void updateMenu(){
  /////////////////////////////////////////////////////////////////////  // JEI PAGRINDINIS MENU
  menuOpenedThreshold =  millis();
  if(menuSection == 0){
    if ( (menuLine & 0x01) == 0) {
      lcd.clear();
      lcd.setCursor(0,0);
      if(menuLine != 0){
        lcd.print(menuStrings[menuLine] + " ");
        lcd.write(127);
      }else
      lcd.print(menuStrings[menuLine] );
      lcd.setCursor(0,1);
      if(menuLine!= ArraySize(menuStrings)-1)
      lcd.print(menuStrings[menuLine+1]);
    } else {
      lcd.clear();
      lcd.setCursor(0,0);
      lcd.print(menuStrings[menuLine - 1]);
      lcd.setCursor(0,1);
      lcd.print(menuStrings[menuLine] + " ");
      lcd.write(127);
    }
  }
  /////////////////////////////////////////////////////////////////////    // JEI SENSITIVITY MENU
  if(menuSection == 1){
    if ( (menuLine & 0x01) == 0) {
      lcd.clear();
      lcd.setCursor(0,0);
      if(menuLine != 0){
        if(menuLine == 4){
          if(ColorOrganOn)
          lcd.print("Turn Off ");
          else
          lcd.print("Turn On ");
          lcd.write(127);
        } else {
          lcd.print(menuSection1Strings[menuLine] + " ");
          lcd.write(127);
        }
      }else
      lcd.print(menuSection1Strings[menuLine] );
      lcd.setCursor(0,1);
      if(menuLine!= ArraySize(menuSection1Strings)-1)
      lcd.print(menuSection1Strings[menuLine+1]);
    } else {
      lcd.clear();
      lcd.setCursor(0,0);
      if(menuLine == 5){
        if(ColorOrganOn)
        lcd.print("Turn Off ");
        else
        lcd.print("Turn On ");
      } else
      lcd.print(menuSection1Strings[menuLine - 1]);
      lcd.setCursor(0,1);
      lcd.print(menuSection1Strings[menuLine] + " ");
      lcd.write(127);
    }
  }
  /////////////////////////////////////////////////////////////////////  // RED GREEN BLUE SELECTIONS
  if(menuSection == 2 || menuSection == 3 || menuSection == 4){
    double dmenuLine = menuLine;
    lcd.clear();
    lcd.setCursor(0,0);
    if(menuSection == 2)
    lcd.print("      RED");
    if(menuSection == 4)
    lcd.print("      BLUE");
    if(menuSection == 3)
    lcd.print("    GREEN");
    lcd.setCursor(0,1);
    lcd.print("Value * ");
    if(menuLine == 20){
      lcd.write('1');
      saveBright(1);
    }
    if(menuLine < 20){
      if(menuLine != 0)
      lcd.print("0.");
      if(menuLine == 1){
        lcd.print("05");
        saveBright(0.05);
      } else if(menuLine == 0){
        lcd.print(0);
        saveBright(0);
      }else {
        lcd.print(menuLine * 5);
        saveBright(dmenuLine * 5 / 100);
      }
    }
    if(menuLine > 20){
      if(menuLine == 21){
        lcd.print("1.0");
        saveBright(1.05);
      }
      else if (menuLine == 40){
        lcd.print("2");
        saveBright(2);
      }
      else
      lcd.print("1.");
      if(menuLine != 40) {
        lcd.print(menuLine * 5 - 100);
        saveBright(dmenuLine * 5 / 100);
      }
    }
  }
  /////////////////////////////////////////////////////////////////////

}



void left() {
  if(menuOpened){
    MenuGoUp();
    // tone(8, 900, 50);
  }
}

void right() {
  if(menuOpened){
    MenuGoDown();
    // tone(8, 800, 50);
  }
}

void Click(){
  menuClick();
  //   tone(8, 1000);
  // delay(50);
  // tone(8, 1100);
  // delay(50);
  // tone(8, 1000);
  //  delay(75);
  // noTone(8);
}

void menuClose(){
  menuOpened = false;
  menuLine=0;
  menuSection = 0;
}
