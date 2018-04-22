using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MangementSys.ProcessRepo;
using System.IO;
using MangementSys.LEGOInterface;

namespace MangementSys.Controllers
{
    //This controller is for Diagnosting Motors/Sensors and the service its self.
    // localhost:5000/Diagno
    public class DiagnoController : Controller
    {
        LegoRobotPS Robot = new LegoRobotPS();

        //GET Service Up, Checking service status.
        [HttpGet]
        public IActionResult CheckService()
        {
            return Ok();
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
