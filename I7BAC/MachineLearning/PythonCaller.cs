using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace I7BAC.MachineLearning
{
    public static class PythonCaller
    {
        public static void CallScript(string path)
        {
            using (FileStream fs = File.Create(@"C:\Users\Mikke\I7BAC\URL.txt"))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(path);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
            
            string myPythonApp = @"C:\Users\Mikke\I7BAC\startPython.bat";

            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(myPythonApp);

            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.CreateNoWindow = true;
            myProcessStartInfo.RedirectStandardOutput = true;

            Process myProcess = new Process();
            myProcess.StartInfo = myProcessStartInfo;

            myProcess.Start();

            myProcess.WaitForExit();

            myProcess.Close();

            //int interval = 5000;
            //Task.Factory.StartNew(() =>
            //{
            //    Thread.Sleep(interval);
            //});
            Aggregator.BroadCastPythonResult(File.ReadAllText(@"C:\Users\Mikke\I7BAC\result.txt"));
        }
    }
}