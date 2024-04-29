using System;
using System.IO;
using System.IO.Ports;

class SerialPortLogger
{
    static void Main()
    {
        SerialPort serialPort = null;
        StreamWriter logger = null;

        try
        {
            string comPort = "COM3";
            int baudRate = 921600;

            serialPort = new SerialPort(comPort, baudRate);
            serialPort.Open();
            Console.WriteLine($"Port {comPort} opened at {baudRate} baud rate.");

            logger = new StreamWriter("Log.txt", append: true);

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
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            if (logger != null)
            {
                logger.Close();
                logger.Dispose();
            }
            if (serialPort != null)
            {
                serialPort.Close();
                serialPort.Dispose();
            }
        }
    }
}
