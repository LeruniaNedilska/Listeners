using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using listeners.Services;

namespace listeners.Repositories
{
    public interface IServiceRepository
    {
        List<IService> GetAll();
        void Add(IService listener);
    }
}
