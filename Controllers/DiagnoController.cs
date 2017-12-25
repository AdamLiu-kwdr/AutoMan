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

        // GET Diagno/TestMotor/interval(Micro Seconds), test motor on port MA. (big motor only.)
        [HttpGet]
        public string TestMotor(int interval)
        {
            throw new Exception("Unfinished function");
            //var res = new RunPY().Run($"{Directory.GetCurrentDirectory()}/run.py","");
            //return res;
        }

        // GET Diagno/TestUC, Test ultra sonic. Will send back UC value.
        [HttpGet]
        public string TestUS()
        {
            throw new Exception("Unfinished function");
            //var res = new RunPY().Run($"{Directory.GetCurrentDirectory()}/run.py","");
            //return res;
        }

        // GET Diagno/TestCS, Test color sensor. Will send back CS value.
        [HttpGet]
        public string TestCS()
        {
            throw new Exception("Unfinished function");
            //var res = new RunPY().Run($"{Directory.GetCurrentDirectory()}/run.py","");
            //return res;
        }

    }
}
