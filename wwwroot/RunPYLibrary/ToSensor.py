#This python script will move Object to the sensor
#by turning conveyor belt until sensor dectes object.
#(conveyor direction is passed in as a parameter.)

import ev3dev.brickpi as ev3
from time import sleep
import threading
import sys

#Timeout, Set to 1 if Sensor not seeing anything after motor timeout.
Tout = 0

def Watchdog():
    global Tout #I need to modify the bolbal copy. Thanks Python. :(
    Tout = 1
    print("sensor timeout")

m = ev3.LargeMotor(ev3.OUTPUT_A)
Co = ev3.ColorSensor(ev3.INPUT_2)
Co.mode="COL-COLOR"

sleep(1)

m.run_timed(time_sp=6000, speed_sp=150)
t = threading.Timer(6.0,Watchdog)
t.start()

#If Sensor not seeing anything and Timer hasn't timeout yet, keep scanning.
while Co.value()!=2 and Tout!=1:
    #print(Co.value())
    sleep(0.2)

if Tout == 1:
  exit(1)

t.cancel()
m.stop()
#print("Co detected")