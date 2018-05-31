using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoManSys.Model;
using AutoManSys.Modules;
using AutoManSys.LEGOInterface;

namespace AutoManSys.Modules
{
    //This is the class to excute the Instruction from OrderManSys.
    //Always pass in Instructions when use this class.
    public class InstructionRunner
    {
        //Creating internal varaibles
        private Communication comm = new Communication();
        private readonly ILEGORobot robot = new LegoRobotPY();
        private int ErrorCount = 0; //For counting failed operation encountered. 
        private readonly IList<Instruction> InstructionSet; //Instructions to excute.

        //Counstructer
        public InstructionRunner(IList<Instruction> instructionset)
        {
            InstructionSet = instructionset;
        }

        //The actual running function. The large pile of switch cases waiting for better soulution.
        //I know this whole approach is pretty dirty, but I can't think a cleaner (while still being fast) way to do this :/
        //So please don't hate me....
        public void Run()
        {
            Console.WriteLine("[Debug] TaskRuner started");
            foreach (var Inst in InstructionSet)
            {
                //If error occured, try the method again until error count reach 3.
                //Sorry, I don't know any better way to do this ;w;
                ErrorRetry:
                try
                {
                //Choose Component.
                switch (Inst.Component)
                {
                    case("Convyor"):
                    {
                        //Convyor Actions: Right, Left, ToSensor.
                        switch (Inst.Action)
                        {
                            case("ToSensor"):
                            {
                                if (Inst.Parameter == "True")
                                {
                                    var result = robot.MovetoSensor(true);
                                    //Console.WriteLine("[Debug]MovetoSensor Forward");
                                    //Console.WriteLine($"[Debug]Result:{result}");
                                }
                                else
                                {
                                    var result = robot.MovetoSensor(false);
                                    //Console.WriteLine("[Debug]MovetoSensor Backward");
                                    //Console.WriteLine($"[Debug]Result:{result}");
                                }
                                break;
                            }

                            case("Right"):
                            {
                                var result = robot.ConveyorMove(true,int.Parse(Inst.Parameter));
                                //Console.WriteLine("[Debug]Conveyor Move Right {0}",Inst.Parameter);
                                //Console.WriteLine($"[Debug]Result:{result}");
                                break;
                            }

                            case("Left"):
                            {
                                var result = robot.ConveyorMove(false,int.Parse(Inst.Parameter));
                                // Console.WriteLine("[Debug]Conveyor Move Left {0}",Inst.Parameter);
                                // Console.WriteLine($"[Debug]Result:{result}");
                                break;
                            }
                        }
                        break;
                    }

                    //Convyor Actions: Next.
                    case("BallLoader"):
                    {
                        switch (Inst.Action)
                        {
                            case("Next"):
                            {
                                var result = robot.BallLoaderNext(int.Parse(Inst.Parameter));
                                // Console.WriteLine("[Debug]BallLoader Next {0}",Inst.Parameter);
                                // Console.WriteLine($"[Debug]Result:{result}");
                                break;
                            }
                        }
                        break;
                    }

                    default:
                    {
                        //Write Log to OrderManSys, wait for the request to complete
                        Task.WaitAll(comm.Send("Communication","LogReport",
                            new Log{
                                type = "Error",
                                Author="AutoManSys:InstructionRunner",
                                Message = $"[Error]Fatal Error:{Inst.Component} is not registered."
                            }
                        ));

                        Console.WriteLine($"[Error]Fatal Error:{Inst.Component} is not registered.");

                        //Unknow component, throw ArgumentException.
                        throw new ArgumentException($"{Inst.Component} is not registered.");
                    }
                }
                }
                catch(Exception e)
                {
                    //Throw Exception if error count reach 3 times.
                    if (ErrorCount == 3)
                    {
                        //Write Log to OrderManSys, wait for the request to complete
                        Task.WaitAll(comm.Send("Communication","LogReport",
                            new Log{
                                type = "Error",
                                Author="AutoManSys:InstructionRunner",
                                Message = $"Failed while running {Inst.Component},{Inst.Action}"
                            }
                        ));
                        Console.WriteLine($"[Error]Failed while running {Inst.Component},{Inst.Action}");
                        throw e;
                    }
                    ErrorCount++;
                    //wait 1 sec.
                    Thread.Sleep(1000);

                    //Sorry, I don't know any better way to do this ;w;
                    goto ErrorRetry;
                }
                // Console.WriteLine($"[Debug]Action successfully carried out for:{Inst.Component},{Inst.Action}");
            }

            //Write Log to OrderManSys, wait for the request to complete
            Task.WaitAll(comm.Send("Communication","LogReport",
                new Log{
                    type = "Info",
                    Author="AutoManSys:InstructionRunner",
                    Message = $"successfully executed Instruction set for product: {InstructionSet.First().Product.ProductName}"
                }
            ));
            // Console.WriteLine($"[Debug]successfully executed Instruction set for product: {InstructionSet.First().Product.ProductName}");
        }
    }
}