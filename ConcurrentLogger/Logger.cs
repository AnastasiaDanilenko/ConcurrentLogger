using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using static ConcurrentLogger.Program;

namespace ConcurrentLogger
{
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
                ThreadPool.QueueUserWorkItem(FlushThreads, logInfo);
                threads++;
                logInfo.Clear();
            }
            logInfo.Add(level.ToString() + message);
        }

        public void FlushThreads(object logs)
        {

        }

    }

    public interface ILogger
    {
        void Log(LogLevel level, string message);

    }
}
