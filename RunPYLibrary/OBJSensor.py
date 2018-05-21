#This program will use Color Sensor on port2. (Please Insert Color Sensor on port 2) 
#Used for detacting if there's object in front of it 
#(This program only returns sensor value.)

import ev3dev.brickpi as ev3 #Import ev3
from time import sleep

#print("Device set up...")
#p2 = ev3.LegoPort(ev3.INPUT_2) #Get control of port S2
#p2.mode ="ev3-uart" #Set port2 to uart mode.
#p2.set_device = "lego-ev3-color" #set port2 to Color Sensor

Co = ev3.ColorSensor(ev3.INPUT_2) #Get control of Color Sensor
Co.mode="COL-COLOR"

sleep(1)

print(Co.value())