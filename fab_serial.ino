int sensor_val_1 = 0;
int sensor_val_2 = 0;
int sensor_val_3 = 0;


void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
}

void loop() {
  // Read sensor data here instead of following dummy three lines
  sensor_val_1 += 1;
  sensor_val_2 += 2;
  sensor_val_3 += 3;
  
  // Print to Serila single value
  Serial.println(sensor_val_1);

  // Print to Serial several values separated by ':'
  // Serial.println(String(sensor_val_1) + ":" + String(sensor_val_2) + ":" + String(sensor_val_3));
   
  
  //delay(20);
}
