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
    public class ProductionController : Controller
    {
        LegoRobotPS Robot = new LegoRobotPS();

        [HttpPost]
        public IActionResult ProduceSingle()
        {
          //TODO: Implement Realistic Implementation
          return Ok();
        }

        [HttpPost]
        public IActionResult ProduceMulti()
        {
          //TODO: Implement Realistic Implementation
          return Ok();
        }
    }
}