namespace MangementSys.LEGOInterface
{
/*
This Interface is for standarlizing the method names/signatures throughout the system.
So it's will be easier add in new BrickPI. 
(Different ID for different BrickPI for the class should be added in the future.)
*/
    public interface ILEGORobot
    {
        //All control methods must return result printed by python.

        string InitialSensors(); //Set Sensor on ports.

        string ConveyorMove(bool GoForward,int Interval); //Move Conveyor belt, goes right, set Inverval
        string BallLoaderNext(int number); //Activate Ball Loader, drop new ball
        string MovetoSensor(bool GoForward); //Move the Package to sensor.

        bool PackageSensor(); //For reading if Color sensor has box in front of it

    }
}