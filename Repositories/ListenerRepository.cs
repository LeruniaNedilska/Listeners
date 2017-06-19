using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using listeners.Repositories;
using listeners.Services;

namespace listeners
{
    public class ServiceRepository: IServiceRepository
    {
        private List<IService> serviceList;

        public ServiceRepository()
        {
            serviceList = new List<IService>();
        }
        public List<IService> GetAll()
        {
            return serviceList;
        }

        public void Add(IService service)
        {
            serviceList.Add(service);
        }
    }
}
