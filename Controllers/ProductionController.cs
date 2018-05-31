using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using AutoManSys.LEGOInterface;

namespace AutoManSys.Controllers
{
    //This controller is a temporary interface for calling production service in a fixed manner.
    //Before The analyze engine is functional, this pre written instructions can be called to let the production line work.
    public class ProductionController : Controller
    {
        private readonly ILEGORobot Robot = new LegoRobotPY();

        [HttpPost]
        public IActionResult ProduceSingle() //Do one production cycle
        {
          Robot.MovetoSensor(true);
          Robot.ConveyorMove(false,400);
          Robot.BallLoaderNext(1);
          Robot.ConveyorMove(true,5000);
          return Ok();
        }
    }
}