using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentLogger
{
    public class Target:ILoggerTarget
    {
        string file;
        public Target(string filename)
        {
            file = filename;
        }
        public bool Flush(string s)
        {
            using ( StreamWriter sw = new StreamWriter(file, true))
            {
                sw.WriteLine(s);
            }
                return true;
        }

        public Task<bool> FlushAsync(string s)
        {
            return new Task<bool>(() => {
                using (StreamWriter sw = new StreamWriter(file))
                {
                    sw.WriteLineAsync(s);
                }
                return true;
            });
        }
    }

    public interface ILoggerTarget
    {
        bool Flush(string s);
        Task<bool> FlushAsync(string s);
    }
}
