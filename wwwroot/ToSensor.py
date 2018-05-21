import ev3dev.brickpi as ev3
from time import sleep

m = ev3.LargeMotor(ev3.OUTPUT_A)
Co = ev3.ColorSensor(ev3.INPUT_2)
Co.mode="COL-COLOR"

sleep(1)

m.run_timed(time_sp=5000, speed_sp=150)
while (Co.value() != 2):
    print(Co.value())
    sleep(0.2)
m.stop()
print("Co detected")