using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using static ConcurrentLogger.Program;

namespace ConcurrentLogger
{
    public class ForThread
    {
        public List<string> logs;
        public int countThreads;
    }
    public class Logger:ILogger
    {
       public  int bufferLimit;
        int actualLoggers;
        public int threads;
        public volatile int i = 1;
        ILoggerTarget[] listTargets = null;
        List<string> logInfo = new List<string>();
        object locker = new object();
        public Logger(int bufferLimit, ILoggerTarget[] targets)
        {
            this.bufferLimit = bufferLimit;
            listTargets = targets;
            actualLoggers = 0;
            threads = 0;
        }

        public void Log(LogLevel level, string message)
        {
            if (actualLoggers == bufferLimit-1)
            {
                threads++;
                ThreadPool.QueueUserWorkItem(FlushThreads, new ForThread { logs = logInfo, countThreads = threads });
                logInfo = new List<string>();
                actualLoggers = 0;
            }
            actualLoggers++;
            logInfo.Add(level.ToString() + message);
        }

        public void FlushThreads(object Info)
        {
            List<string> listLogs = ((ForThread)Info).logs;
            int count = ((ForThread)Info).countThreads;
            Console.WriteLine("Thread number " + count + " entered the critical area");
            Monitor.Enter(locker);          
                while (i != count)
                    Monitor.Wait(locker);
            Console.WriteLine("Thread number " + count + " is writing its logs");
            foreach (string s in listLogs)
                    listTargets[0].Flush(s);
            i++;
            Monitor.PulseAll(locker);
            Monitor.Exit(locker);
        }

    }

    public interface ILogger
    {
        void Log(LogLevel level, string message);

    }
}
