using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentLogger
{
    public class Target:ILoggerTarget
    {
        public bool Flush()
        {
            return true;
        }

       public bool fuc()
        {
            return true;
        }

        public Task<bool> FlushAsync()
        {
            return new Task<bool>(fuc
                );
        }
    }

    public interface ILoggerTarget
    {
        bool Flush();
        Task<bool> FlushAsync();
    }
}
