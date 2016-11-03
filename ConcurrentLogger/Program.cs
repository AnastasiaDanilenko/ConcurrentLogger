using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentLogger
{
    public class Program
    {
        public enum LogLevel
        {
            Debug,
            Info,
            Warning,
            Error
        }

        static void Main(string[] args)
        {
            Target loggerFile = new Target("LoggerFile.txt");
            ILoggerTarget[] loggerTargets = new ILoggerTarget[] { loggerFile };
            Console.WriteLine("Limit:");
            int limit = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Loggers:");
            int loggers = Convert.ToInt32(Console.ReadLine());
            Logger logger = new Logger(limit, loggerTargets);
            for (int i = 1; i<loggers; i++)
            {
                logger.Log(LogLevel.Info, DateTime.Now.ToString() +" " + i.ToString());
            }
            Console.ReadKey();
        }

    }
}
