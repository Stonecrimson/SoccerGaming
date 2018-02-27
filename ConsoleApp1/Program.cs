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
        static string file;
        static string testNumber;
        static string hits;
        static int trials;
        static long time; 
        static void Main(string[] args)
        {
            timer = new Stopwatch(); 
            Console.WriteLine("Filename?");
            testNumber = Console.ReadLine();
            Console.WriteLine("Number of Trials?");
            int.TryParse(Console.ReadLine(), out trials);
            hits = "";  
            timer.Start(); 
                SerialPort sp = new SerialPort("COM3", 115200);

                sp.Open();
            sp.DiscardInBuffer(); 
            sp.DataReceived += Sp_DataReceived;
             


            Console.WriteLine("Press any key to write to file and exit, do not exit until tests are complete");
            Console.ReadKey();

            string filepath = @"C:\Users\Public\TestResults\" + testNumber + ".csv";
            char[] array =hits.ToCharArray();
            System.IO.File.WriteAllText(filepath, hits); 

            timer.Stop(); 
        }

        private static void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
         
            if(counter == trials)
            {
                Console.WriteLine("Test Complete"); 
                return; 
            }
            Thread.Sleep(5); 
            SerialPort sp = (SerialPort)sender;
            while (sp.BytesToRead > 0)
            {
                hits += sp.ReadExisting(); 
            }

        }
    }
}
