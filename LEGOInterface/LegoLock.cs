using System;

//This is the static class controlling the Hardware lock
/*
Methods:
getLockStatus()
tryLock()
unLock()
 */
namespace AutoManSys.LEGOInterface
{
    public static class LegoLock
    {
        //Actual Lock varaible. True: Lego is being used.
        private static bool _Hardwarelock;

        //Locker name, storing the id of thread that locked the lego.
        private static string _LockerName;

        //return lock 
        public static bool getLockStatus()
        {
            return _Hardwarelock;
        }

        //try locking the Lego, 0:lock failed.
        public static int tryLock(string lockerName)
        {
            if (_Hardwarelock == false)
            {   
                _Hardwarelock = true;
                _LockerName = lockerName;
                return 1;
            }
            return 0;
            
        }
        
        //try unlocking the Lego, 0:lock failed.
        public static int unLock(string lockerName)
        {
            if (_LockerName == lockerName)
            {
                _Hardwarelock = false;
                return 1;
            }
            return 0;
        }
    }
}