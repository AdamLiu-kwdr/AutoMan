using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoManSys.Model;
using AutoManSys.PYRepo;
using AutoManSys.TaskRunner;
using AutoManSys.LEGOInterface;

namespace AutoManSys.Controllers
{
    //This controller is the actual controller to accept instruction sets from OrdermanSys
    //Should actions done in async way?
    public class TaskRunnerController : Controller
    {
        //Checking if service's ready.
        [HttpGet]
        public IActionResult CheckService()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Execute([FromBody]IEnumerable<Instruction> Insts)
        {
            TaskRunnerEngine TrunnerEngine = new TaskRunnerEngine(Insts.ToList());
            Thread RunnerThread = new Thread(TrunnerEngine.Run);
            RunnerThread.Start();
            return StatusCode(202);
        }
    }
}