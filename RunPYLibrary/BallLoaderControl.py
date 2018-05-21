#This program activate the ball loader, working cycle will be passed in as parameter.
#(Eg. how many balls are dropped.)

import ev3dev.brickpi as ev3 #Import ev3
import sys # import sys library for arguments function
from time import sleep

#ARGS: BallLoaderControl.py {Times}

m = ev3.LargeMotor(ev3.OUTPUT_B) #Initalize Motor on Port MB
if int(sys.argv[1]) <= 0:
    sys.argv[1] = 1

print("BallLoader Running. Unload %(times)s Balls" %{'times':sys.argv[1]})

for x in range(0,int(sys.argv[1])):
    m.run_to_rel_pos(position_sp=-90, speed_sp=700)
    sleep(0.8)