#This class is for initalizing the color sensor, also checking if motors are usuable in python.

import ev3dev.brickpi as ev3
from time import sleep

#print("Initializing...")
#print("Loading Drivers...")
p2 = ev3.LegoPort(ev3.INPUT_2)
p2.mode ="ev3-uart"
p2.set_device = "lego-ev3-color"
print("Device set up...")
Belt = ev3.LargeMotor(ev3.OUTPUT_A)
Loader = ev3.LargeMotor(ev3.OUTPUT_A)
Co = ev3.ColorSensor(ev3.INPUT_2)
Co.mode="COL-COLOR"
print("Initialization completed.")

