using System;
using System.IO;
using AutoManSys.PYRepo;

namespace AutoManSys.LEGOInterface
{
    /*
    This class is the ILEGORobot's PYthon implement.
    will control LEGO through PYrepo's RunPY class.
    SensorStart.py => Initial Sensors
    BeltControl.py => Control Convey Belt
    BallLoaderControl.py => Control Ball Loader
    (PS means Python Scripts)
    */
    public class LegoRobotPY : ILEGORobot
    {

       /*  Reserve for future use.
        private string BeltMotorPort = "OUTPUT_A"; //Motor on MA
        private string BallLoaderMotorPort = "OUTPUT_B"; //Motor on MB
        private string ObjectSensorPort = "INPUT_2"; //Motor on S2
        */

        public string InitialSensors() //Set Sensor on ports.
        {
            var result = new RunPY().Run($"{Directory.GetCurrentDirectory()}/wwwroot/RunPYLibrary/SensorStart.py",null);
            return result;
        }

        public string ConveyorMove(bool GoForward,int Interval) //Move Conveyor belt, goes right, set Inverval
        {
            int speed = 100;
            switch (GoForward)
            {
                case false: 
                    speed = speed* -1;
                    break;
                case true:
                    break;
            }
            var result = new RunPY().Run($"{Directory.GetCurrentDirectory()}/wwwroot/RunPYLibrary/BeltControl.py",$"{Interval}\" \"{speed}");
            return result;
        }
        public string BallLoaderNext(int number = 1) //Activate Ball Loader, drop new ball
        {
            var result = new RunPY().Run($"{Directory.GetCurrentDirectory()}/wwwroot/RunPYLibrary/BallLoaderControl.py",$"{number}");
            return result;
        }
        public string MovetoSensor(bool GoForward = true) //Move the Package to sensor.
        {
            int speed = 100;
            switch (GoForward)
            {
                case false: 
                    speed = speed* -1;
                    break;
                case true:
                    break;
            }
            var result = new RunPY().Run($"{Directory.GetCurrentDirectory()}/wwwroot/RunPYLibrary/ToSensor.py",$"{speed}");
            return result;
        }

        public bool PackageSensor() //For reading if Color sensor has box in front of it
        {
            var result = new RunPY().Run($"{Directory.GetCurrentDirectory()}/wwwroot/RunPYLibrary/OBJSensor.py",null);
            bool reading = false;
            if(int.Parse(result) == 2){
                reading = true;
            }
            return reading;
        }
    }
}