using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConcurrentLogger.Program;

namespace ConcurrentLogger
{
    public class Logger:ILogger
    {
        int bufferLimit;
        ILoggerTarget[] listTargets = null;
        public Logger(int bufferLimit, ILoggerTarget[] targets)
        {
            this.bufferLimit = bufferLimit;
            listTargets = targets;
        }

        public void Log(LogLevel level, string message)
        {

        }
    }

    public interface ILogger
    {
        void Log(LogLevel level, string message);

    }
}
