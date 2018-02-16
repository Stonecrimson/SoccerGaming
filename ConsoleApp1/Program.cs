using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading; 
using System.IO.Ports;

using SoccerGaming;  

namespace DataLogger
{
    class Program
    {

        static int counter = 0;
        static string file;
        static string testNumber;
        static string[] hits;
        static int trials; 
        static void Main(string[] args)
        {
            Console.WriteLine("Test Number?");
            testNumber = Console.ReadLine();
            Console.WriteLine("Number of Trials?");
            int.TryParse(Console.ReadLine(), out trials);
            hits = new string[trials]; 
            
                SerialPort sp = new SerialPort("COM3", 9600);

                sp.Open();
            sp.DiscardInBuffer(); 
            sp.DataReceived += Sp_DataReceived;
             


            Console.WriteLine("Press any key to write to file and exit, do not exit until tests are complete");
            Console.ReadKey();

            System.IO.File.WriteAllLines(@"C:\Users\Public\TestResults\Test" + testNumber + ".csv", hits);


        }

        private static void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
         
            if(counter == trials)
            {
                Console.WriteLine("Test Complete"); 
                return; 
            }
            Thread.Sleep(50); 
            SerialPort sp = (SerialPort)sender;
            string data = sp.ReadExisting();
            if (data[0] == 'H')
            {
                Console.WriteLine("HIT");
                char[] sep = { ' ', ',' }; 
                string[] toks = data.Split(sep);
                string newValue = toks[1] + "," + toks[2]; 
                hits[counter] = newValue;
            }
            counter++; 
        }
    }
}
