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
        int bufferLimit;
        int actualLoggers;
        int threads;
        ILoggerTarget[] listTargets = null;
        List<string> logInfo = new List<string>();
        public Logger(int bufferLimit, ILoggerTarget[] targets)
        {
            this.bufferLimit = bufferLimit;
            listTargets = targets;
            actualLoggers = 0;
            threads = 0;
        }

        public void Log(LogLevel level, string message)
        {
            actualLoggers++;
            if (actualLoggers == bufferLimit)
            {
                ThreadPool.QueueUserWorkItem(FlushThreads, new ForThread { logs = logInfo, countThreads = threads });
                threads++;
                logInfo.Clear();
            }
            logInfo.Add(level.ToString() + message);
        }

        public void FlushThreads(object Info)
        {
            List<string> listLogs = ((ForThread)Info).logs;
            int count = ((ForThread)Info).countThreads;
            lock (locker)
            {
                while (threadInfo.ThreadId != currentBufferId)
                    Monitor.Wait(locker);
                foreach (ILoggerTarget currentTarget in targets)
                    foreach (LogInfo log in logsList)
                        currentTarget.Flush(log);
                currentBufferId++;
                Monitor.PulseAll(locker);
            }
        }

    }

    public interface ILogger
    {
        void Log(LogLevel level, string message);

    }
}
