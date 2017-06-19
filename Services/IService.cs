using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using listeners.Models;

namespace listeners.Services
{
    public interface IService
    {
        int Type { get; }
        string Name { get; }
        DateTime LastAction { get; }
        FixedQueue<IMessage> Last { get; }
    }
}
