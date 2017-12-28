using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MangementSys.ProcessRepo;
using System.IO;

namespace MangementSys.Controllers
{
    // localhost:5000/Diagno
    public class DiagnoController : Controller
    {
        //GET Service Up, Checking service status.
        [HttpGet]
        public IActionResult CheckService()
        {
            return Ok();
        }

        // GET Diagno/TestMotor/interval(Micro Seconds, default 1000), test motor on port MA. (big motor only.)
        [HttpGet]
        public string TestMotor(int Interval = 1000)
        {
            var result = new RunPY().Run($"{Directory.GetCurrentDirectory()}/TestMotor.py",$"{Interval}");
            return result;
        }

        // GET Diagno/TestUC, Test ultra sonic. Will send back UC value.
        [HttpGet]
        public string TestUS()
        {
            var result = new RunPY().Run($"{Directory.GetCurrentDirectory()}/TestUC.py","");
            return result;
        }

        // GET Diagno/TestCS, Test color sensor. Will send back CS value.
        [HttpGet]
        public string TestCS()
        {
            var result = new RunPY().Run($"{Directory.GetCurrentDirectory()}/TestCS.py","");
            return result;
        }

    }
}
