using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading; 
using System.IO.Ports;
using System.Diagnostics; 

using SoccerGaming;  

namespace DataLogger
{
    class Program
    {
        static Stopwatch timer; 
        static int counter = 0;
        static int totalCounter = 0; 
        static string file;
        static string testNumber;
        static string[] hits;
        static int trials;
        static long[] times;
        static string[] strings; static int finalCount; 
        //static long time; 
        static void Main(string[] args)
        {
            
            timer = new Stopwatch(); 
            Console.WriteLine("Filename?");
            testNumber = Console.ReadLine();
            Console.WriteLine("Number of Trials?");
            int.TryParse(Console.ReadLine(), out trials);
            hits = new string[10*trials];
            times = new long[10*trials]; strings = new string[10*trials]; 
            timer.Start(); 
                SerialPort sp = new SerialPort("COM3", 115200);

                sp.Open();
            sp.DiscardInBuffer(); 
            sp.DataReceived += Sp_TargetDataReceived;
             


            Console.WriteLine("Press any key to write to file and exit, do not exit until tests are complete");
            sp.DiscardInBuffer();

            Console.ReadKey();

            string filepath = @"C:\Users\Public\TestResults\" + testNumber + ".csv";
            for (int i = 0; i < counter - 1; i++)
            {
                string temp = "";
                char[] split = { ',', ' ' };
                string[] toks = (hits[i]).Split(split);
                if (toks.Length == 3)
                {
                    temp = times[i].ToString() + "," + toks[0] + "," + toks[1];
                    strings[i] = temp;
                }
            }
            System.IO.File.WriteAllLines(filepath, strings);  
              
            timer.Stop(); 
        }

        private static void Sp_TargetDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
            
            if (totalCounter >= trials)
            {
                Console.WriteLine("Test Complete");
                return;
            }
            times[counter] = timer.ElapsedMilliseconds;
            long diff = 0; 
            if (counter > 0)
            {
              diff = times[counter] - times[counter - 1];
            }
            
            Console.WriteLine("HIT");
           //Thread.Sleep(5);
           SerialPort sp = (SerialPort)sender;
           hits[counter] = sp.ReadLine();
           counter++; 
            //timer.Restart(); 
            if(diff > 300)
                totalCounter++;
        }
    }
}
