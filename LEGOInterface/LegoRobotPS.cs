using System;
using System.IO;
using MangementSys.ProcessRepo;

namespace MangementSys.LEGOInterface
{
    /*
    This Interface controls LEGO by running python through RunPY.
    SensorStart.py => Initial Sensors
    BeltControl.py => Control Convey Belt
    BallLoaderControl.py => Control Ball Loader
    */
    public class LegoRobotPS : ILEGORobot
    {
        private string BeltMotorPort = "OUTPUT_A"; //Motor on MA
        private string BallLoaderMotorPort = "OUTPUT_B"; //Motor on MB
        private string ObjectSensorPort = "INPUT_2"; //Motor on S2

        public string InitialSensors() //Set Sensor on ports.
        {
            var result = new RunPY().Run($"{Directory.GetCurrentDirectory()}/SensorStart.py",null);
            return result;
        }

        public string ConveyorMove(bool GoForward,int Interval) //Move Conveyor belt, goes right, set Inverval
        {
            var result = new RunPY().Run($"{Directory.GetCurrentDirectory()}/BeltControl.py",$"{Interval} {BeltMotorPort}");
            return result;
        }
        public string BallLoaderNext(int number = 1) //Activate Ball Loader, drop new ball
        {
            var result = new RunPY().Run($"{Directory.GetCurrentDirectory()}/BallLoaderControl.py",$"{number} {BallLoaderMotorPort}");
            return result;
        }
        public string MovetoSensor(bool GoForward = true) //Move the Package to sensor.
        {
            var result = new RunPY().Run($"{Directory.GetCurrentDirectory()}/ToSensor.py",$"{GoForward} {BeltMotorPort} {ObjectSensorPort}");
            return result;
        }

        public bool PackageSensor() //For reading if Color sensor has box in front of it
        {
            bool reading = false;
            return reading;
        }
    }
}