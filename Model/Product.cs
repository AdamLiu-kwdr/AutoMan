using System;

namespace AutoManSys.Model
{
    //Model class come with Instructions.
    public class Product
    {
        public int Id{get; set;} //Primary Key
        public string ProductName{get; set;}
        public string Description{get; set;}
        public double Price{get; set;}

    }
}