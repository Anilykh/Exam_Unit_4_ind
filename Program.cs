﻿using System;
using System.IO.Ports;

class SerialPortLogger
{
    static void Main()
    {
        string comPort = "COM3"; // Change the COM port to match your setup
        int baudRate = 921600; // Change the baud rate as needed

        using (SerialPort serialPort = new SerialPort(comPort, baudRate))
        {
            try
            {
                serialPort.Open();
                Console.WriteLine($"Port {comPort} opened at {baudRate} baud rate.");

                using (StreamWriter logger = new StreamWriter("Log.txt", append: true))
                {
                    while (true)
                    {
                        if (serialPort.BytesToRead > 0)
                        {
                            string logEntry = serialPort.ReadLine();
                            Console.WriteLine(logEntry);
                            logger.WriteLine($"{DateTime.Now}: {logEntry}");
                            logger.Flush();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}