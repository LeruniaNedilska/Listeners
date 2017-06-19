using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace listeners.Services
{
    public interface IListener : IService
    {
        int Port { get; }
        void Start();
        void Stop();
        void Log();
    }
}
