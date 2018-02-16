using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports; 
using SoccerGaming;  

namespace ConsoleApp1
{
    class Program
    {
        
        

        static void Main(string[] args)
        {
            
                SerialPort sp = new SerialPort("COM3", 9600);

                sp.Open();
                sp.NewLine = "XX";

                byte[] buf = new byte[24];
            
            while (true)
            {
                if (sp.IsOpen)
                {
                  sp.Read(buf, 0,10);
                    string something = Encoding.ASCII.GetString(buf); 
                    if (buf[0] == (byte) 'H')
                    {
                        Console.WriteLine("Succeded: " +  something + "\n");
                    }

                    
                }

            }

            
        }
    }
}
