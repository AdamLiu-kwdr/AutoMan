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

        [HttpPost]
        public IActionResult Execute([FromBody]IEnumerable<Instruction> Insts)
        {
            InstructionRunner Trunner = new InstructionRunner(Insts.ToList(),_connectionstrings);
            // Thread RunnerThread = new Thread(TrunnerEngine.Run);
            // RunnerThread.Start();
            Task.Factory.StartNew(Trunner.Run).ConfigureAwait(false);
            return StatusCode(202);
        }
    }
}