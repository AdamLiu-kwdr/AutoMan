using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using AutoManSys.LEGOInterface;


namespace AutoManSys.Controllers
{
    //This controller is for Diagnosting Motors/Sensors and the service its self.
    // localhost:5000/Diagno
    public class DiagnoController : Controller
    {
        private readonly ILEGORobot Robot = new LegoRobotPY();

        //GET Service Up, Checking service status.
        [HttpGet]
        public string CheckService()
        {
            var result = Robot.InitialSensors();
            return (result);
        }

        // GET Diagno/TestMotor?interval=1000 (Micro Seconds, default 1000), test motor on port MA. (big motor only.)
        [HttpGet]
        public string TestMotors(int Interval = 1000)
        {
            var BeltResult = Robot.ConveyorMove(true,Interval);
            var LoaderResult = Robot.BallLoaderNext(1);
            return (BeltResult+LoaderResult);
        }

        // GET Diagno/TestCS, Test Object sensor(ColorSensor). Will return if object detacted.
        [HttpGet]
        public string TestOBJsensor()
        {
            var result = Robot.PackageSensor();
            return $"ObjectSensor result: {result}";
        }
    }
}
