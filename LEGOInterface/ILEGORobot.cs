namespace MangementSys.LEGOInterface
{
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