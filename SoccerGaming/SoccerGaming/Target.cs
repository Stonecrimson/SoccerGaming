using System;
using System.IO; 
using System.IO.Ports;

/// <summary>
/// Target Wrapper class for the soccer gamification project. 
/// Benjamin Stewart, 2017
/// </summary>





namespace SoccerGaming
{
    // The wrapper abstract class for the target. This class holds the values of the last hit location and helper functions to control the lighting in feedback.
    
    //IN Progress as of 10/16/2017 (Ben Stewart notes) 
    // Still trying to expose the DataRecieved event to any use cases. 
    // include  hit speed when the ultrasonic sensors are implemented.
    // angle will have to be through CV. I can try to come up with something, but implementation will not be easy or cheap. 
    // 
    public class Target
    {
        //private fields. I plan on only exposing the functionality you need. 
        private SerialPort targetPort;
        private int[] hitLocation;
        private Boolean didHit;
        private double hitSpeed;
        private int hitCount; 
        


        //Constructs a serial port to handle the backend. All you need to know is the COM port. 
        //ie 
        //Target firstTarget = new Target("COM1",9600); 
        public Target(string port, int baudRate)
        {
            targetPort = new SerialPort(port);

            targetPort.BaudRate = baudRate;
            targetPort.Parity = Parity.None;
            targetPort.StopBits = StopBits.One;
            targetPort.DataBits = 8;
            targetPort.Handshake = Handshake.None;
            targetPort.RtsEnable = true;

            targetPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            targetPort.Open(); 
        }

        
        //defaults to 9600 baudrate
        public Target(string port)
        {
            targetPort = new SerialPort(port);

            targetPort.BaudRate = 9600;
            targetPort.Parity = Parity.None;
            targetPort.StopBits = StopBits.One;
            targetPort.DataBits = 8;
            targetPort.Handshake = Handshake.None;
            targetPort.RtsEnable = true;

            targetPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            targetPort.Open(); 
        }

         ~Target()
        {
            targetPort.Close(); 
        }


        // Simply retrieves the coordinates of the last hit
        public int[] getLastHit() 
        {
            return hitLocation; 
        }

        


        //SerialDataRecieved Evnt handler. Still trying to figure out how to use this to expose a flag for you guys. 
        private void DataReceivedHandler(
                     object sender,
                     SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            if (indata.Contains("HIT"))
            {
                String[] tok = new String[3];
                string[] stringSeparators = { ",", ".", "!", "?", ";", ":", " " };
                tok = indata.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                if (tok.Length == 3)
                {
                   int.TryParse(tok[1], out this.hitLocation[0]);
                   int.TryParse(tok[2], out this.hitLocation[1]);
                    hitCount++; 
                }
                 Console.WriteLine("DataRead = " + indata);

             }
        }

        //PLan on implementing in the near future. Starts the timer on the arduino
        public void startTimer()
        {

        }
        //Returns the number of touches on the target
        public int getHitCount()
        {
            return hitCount; 
        }

        /*Ok First a little Explanation. I plan on putting four addressable LED bars (or one big long one that i abstractly divide into 4 segments) 
         *on the target. WE can have them be timers, number of hits, give feedback, etc. Basically, what I plan on implementing as soon as I can is
         *having all logic done in the game, and we try to use the arduino as a black box middleman to handle the hardware outputs. We can send it a number of set 
         * arguments such as color, fill size, light behavior, etc. I can add functionality as seen fit, but for now, I'm limiting it to behave like a meter 
         * that fills up with a selected color, given a value from 1 (empty) to  100 (full) 
        *
        */

        public void setRightBar(int fill, String color) {
           
        }
        public void setLeftBar(int fill, String color)
        {
           
        }
        public void setTopBar(int fill, String color)
        {
         
        }
        public void setBottomBar(int fill, String color)
        {
            
        }





    }

}
