using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoManSys.Model;
using AutoManSys.LEGOInterface;

namespace AutoManSys.TaskRunner
{
    //This is the class to excute the Instruction from OrderManSys.
    //Always pass in Instructions when use this class.
    public class TaskRunnerEngine
    {
        //Creating internal varaibles
        private readonly ILEGORobot robot = new LegoRobotPY();
        private int ErrorCount = 0; //For counting failed operation encountered. 
        private readonly IList<Instruction> InstructionSet; //Instructions to excute.

        //Counstructer
        public TaskRunnerEngine(IList<Instruction> instructionset)
        {
            InstructionSet = instructionset;
        }

        //The actual running function. The large pile of switch cases waiting for better soulution.
        //I know this whole approach is pretty dirty, but I can't think a cleaner (while still being fast) way to do this :/
        //So please don't hate me....pretty please?
        public void Run()
        {
            foreach (var Inst in InstructionSet)
            {
                //Sorry, I don't know any better way to do this ;w;
                //If error occured, try the method again until error count reach 3.
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
                                    //robot.MovetoSensor(true);
                                    Console.WriteLine("[Debug]MovetoSensor Forward");
                                }
                                else
                                {
                                    //robot.MovetoSensor(false);
                                    Console.WriteLine("[Debug]MovetoSensor Backward");
                                }
                                break;
                            }

                            case("Right"):
                            {
                                //robot.ConveyorMove(true,int.Parse(Inst.Parameter));
                                Console.WriteLine("[Debug]Conveyor Move Right {0}",Inst.Parameter);
                                break;
                            }

                            case("Left"):
                            {
                                //robot.ConveyorMove(false,int.Parse(Inst.Parameter));
                                Console.WriteLine("[Debug]Conveyor Move Left {0}",Inst.Parameter);
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
                                //robot.BallLoaderNext(int.Parse(Inst.Parameter));
                                Console.WriteLine("[Debug]BallLoader Next {0}",Inst.Parameter);
                                break;
                            }
                        }
                        break;
                    }

                    default:
                    {
                        //Unknow component, throw ArgumentException.
                        Console.WriteLine($"[Debug]Fatal Error:{Inst.Component} is not registered.");
                        throw new ArgumentException($"{Inst.Component} is not registered.");
                    }
                }
                }
                catch(Exception e)
                {
                    //Throw Exception if error count reach 3 times.
                    if (ErrorCount == 3)
                    {
                        Console.WriteLine($"[Debug]Failed while doing:{Inst.Component},{Inst.Action}");
                        throw e;
                    }
                    ErrorCount++;
                    //wait 1 sec.
                    Thread.Sleep(1000);

                    //Sorry, I don't know any better way to do this ;w;
                    goto ErrorRetry;
                }
                Console.WriteLine($"[Debug]Action successfully carried out for:{Inst.Component},{Inst.Action}");
            }
            Console.WriteLine($"[Debug]successfully executed Instruction set for product: {InstructionSet.First().Product.ProductName}");
        }
    }
}