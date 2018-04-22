using System.Diagnostics;
using System.IO;

namespace AutoManSys.ProcessRepo
{
    /*
    This class is For starting a new thread for Python runtime.
    So the python API driver iis actually called from here.
    All the pre written Python program are storeed in RunPy.
    */
    public class RunPY
    {
        //Call like this:
        //var res = new RunPY().Run($"{Directory.GetCurrentDirectory()}/run.py","");
        //Console.WriteLine(res);

        public string Run(string cmd, string args)
        {
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = "python3"; //Starting Python Runtime from /usr/local/bin
        start.Arguments = string.Format("\"{0}\" \"{1}\"", cmd, args); //Attaching actualy script path, and arguments.
        start.UseShellExecute = false;// Do not use OS shell
        start.CreateNoWindow = true; // We don't need new window
        start.RedirectStandardOutput = true;// Any output, generated by application will be redirected back
        start.RedirectStandardError = true; // Any error in standard output will be redirected back (for example exceptions)
        using (Process process = Process.Start(start))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string stderr = process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script
                string result = reader.ReadToEnd(); // Here is the result of StdOut(for example: print "test")
                return result;
            }
        }
    }
    }
}