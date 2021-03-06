using System;

namespace AutoManSys.Model
{
    //This model class is for Instructions in the AutoManSys's InstructionRunner and  
    //OrderManSys's Analyze engine. contains running instructions for lego motors.

    //Version 0.1
    public class Instruction
    {
        public int id; //Unique ID
        public int Step; //Instruction's sequence
        public string Component; //Selecting the machines to use (Ballloader,Convyer etc...)
        public string Action; //Which action the selected machine need to do
        public string Parameter; //action's paraments.
        public Product Product;
    }
}