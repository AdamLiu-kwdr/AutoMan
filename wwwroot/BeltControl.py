import ev3dev.brickpi as ev3 #Import ev3
import sys # import sys library for arguments function

#ARGS: BeltControl.py {Intervel} {Speed}

m = ev3.LargeMotor(ev3.OUTPUT_A) #Initalize Motor on Port MA
if sys.argv[1] == 0:
    sys.argv[1] = 1000

print("Motor Running. Runs on speed %(speed)s for: %(time)s microSeconds" %{'speed':sys.argv[2],'time':sys.argv[1]})

m.run_timed(time_sp=sys.argv[1], speed_sp=sys.argv[2])
