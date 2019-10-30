using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication21.Dtos;

namespace WebApplication21.Data
{
    public class FileRepository
    {
        public  bool runFile(string Filename)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.Verb = "runas";
                //psi.UserName = "";
                //psi.Password = "";
                //psi.Arguments = "-jar -XX:+UseConcMarkSweepGC -Xmx1024M -Xms1024M START.jar";
                psi.UseShellExecute = true;
                psi.CreateNoWindow = true;
                psi.WindowStyle = ProcessWindowStyle.Maximized;
                //psi.FileName =  @"C:\Users\Meta\Desktop\ejemplo.bat";
                psi.FileName = @Filename;
                var ok = psi.FileName;
                Process.Start(psi);

                return true;
            }
            catch (Exception e)
            {

                return false;
            }
            
        }
        public string runFile2(FileDto file)
        {
            string RUTA = @"D:\Vigilar";
            string rutaTxt = @RUTA + @"\" + file.FileName+ ".bat";

            try
            {
                FileInfo fileInfo = new FileInfo(rutaTxt);
                if (fileInfo.Exists) fileInfo.Delete();
                using (StreamWriter sw = fileInfo.CreateText())
                {
   
                    sw.WriteLine("@echo off");
                    sw.WriteLine("\"C:\\Program Files (x86)\\ACL Software\\ACL for Windows 14\\ACLWin.exe\"" + " \"D:\\ACL\\INGRESO DOLARES\\Prueba.ACL\"" + " /vProject=\""+file.param1+" \" /bInicio") ;
                }

                return "TRUE";



            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public bool runFilePython(string Filename)
        {
            try
            {
                string python = @"C:\Program Files (x86)\Python37-32\python.exe";

                // python app to call 
                string myPythonApp = @"C:\Users\contingencia\Desktop\prueba\filter.py";

                // dummy parameters to send Python script 
                int x = 2;
                int y = 5;

                // Create new process start info 
                ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(python);

                // make sure we can read the output from stdout 
                myProcessStartInfo.UseShellExecute = false;
                myProcessStartInfo.RedirectStandardOutput = true;

                // start python app with 3 arguments  
                // 1st arguments is pointer to itself,  
                // 2nd and 3rd are actual arguments we want to send 
                myProcessStartInfo.Arguments = myPythonApp;
                    // + " " + x + " " + y;

                Process myProcess = new Process();
                // assign start information to the process 
                myProcess.StartInfo = myProcessStartInfo;

                Console.WriteLine("Calling Python script with arguments {0} and {1}", x, y);
                // start the process 
                myProcess.Start();

                // Read the standard output of the app we called.  
                // in order to avoid deadlock we will read output first 
                // and then wait for process terminate: 
                StreamReader myStreamReader = myProcess.StandardOutput;
                string myString = myStreamReader.ReadLine();

                /*if you need to read multiple lines, you might use: 
                    string myString = myStreamReader.ReadToEnd() */

                // wait exit signal from the app we called and then close it. 
                myProcess.WaitForExit();
                myProcess.Close();

                // write the output we got from python app 
                Console.WriteLine("Value received from script: " + myString);

                return true;

                //string fileName = @"C: \Users\contingencia\Desktop\prueba\filter.py";
                //ProcessStartInfo start = new ProcessStartInfo();
                //start.FileName = @"C:\Program Files (x86)\Python37-32\python.exe";
                //start.Arguments = string.Format("{0} {1}", fileName, "");
                //start.UseShellExecute = false;
                //start.RedirectStandardOutput = true;
                //using (Process process = Process.Start(start))
                //{
                //    using (StreamReader reader = process.StandardOutput)
                //    {
                //        string result = reader.ReadToEnd();
                //        Console.Write(result);
                //    }
                //}
                //return true;

            //string fileName = @"C: \Users\contingencia\Desktop\prueba\filter.py";

            //Process p = new Process();
            //p.StartInfo = new ProcessStartInfo(@"C:\Program Files (x86)\Python37-32\python.exe", fileName)
            //{
            //    RedirectStandardOutput = true,
            //    UseShellExecute = false,
            //    CreateNoWindow = true
            //};
            //p.Start();

            //string output = p.StandardOutput.ReadToEnd();
            //p.WaitForExit();

            //Console.WriteLine(output);

            ////Console.ReadLine();
            //return true;

        }
            catch (Exception e)
            {

                return false;
            }

        }
    }
}
