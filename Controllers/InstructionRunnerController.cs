using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using AutoManSys.Model;
using AutoManSys.Modules;
using AutoManSys.LEGOInterface;

namespace AutoManSys.Controllers
{
    //This controller is the actual controller to accept instruction sets from OrdermanSys
    //Should actions done in async way?
    public class InstructionRunnerController : Controller
    {
        private readonly ConnectionStringOption _connectionstrings;
        public InstructionRunnerController(IOptions<ConnectionStringOption> ConnectionString)
        {
            _connectionstrings = ConnectionString.Value;
        }

        //Checking if service's ready.
        [HttpGet]
        public IActionResult CheckService()
        {
            return Ok();
        }

        //The function to activate AutoManSys' production line.
        [HttpPost]
        public IActionResult Execute([FromBody]IEnumerable<Instruction> Insts,[FromQuery(Name = "Contiune")]bool Contiune = false)
        {
            if (LegoLock.getLockStatus() == true)
            {
                return StatusCode(503,"Lego is running");
            }
            InstructionRunner Irunner = new InstructionRunner(Insts.ToList(),_connectionstrings,Contiune,Guid.NewGuid().ToString());
            // Thread RunnerThread = new Thread(TrunnerEngine.Run);
            // RunnerThread.Start();
            Task.Factory.StartNew(Irunner.Run).ConfigureAwait(false);
            return StatusCode(202);
        }

        [HttpGet("/Lock")]
        public IActionResult Lock()
        {
          return Ok($"Lego Lock: {LegoLock.getLockStatus()}");
        }

        [HttpGet("/FlipLock")]
        public IActionResult FlipLock()
        {
          if (LegoLock.getLockStatus() == true)
          {
              LegoLock.unLock("Controller");
          }
          else
          {
            LegoLock.tryLock("Controller");   
          }
          return Ok();
        }
    }
}