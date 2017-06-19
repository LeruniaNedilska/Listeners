using Microsoft.AspNetCore.SignalR;
using System;
using listeners.Repositories;
using Microsoft.Extensions.DependencyInjection;
using listeners.Models;

namespace listeners
{
    public class ListenerHub : Hub<IListenerHub>
    {
        private readonly IServiceRepository _serviceRepository;

        public ListenerHub(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public void AddMessage(string message, int type, string name)
        {
            int index = _serviceRepository.GetAll().IndexOf(_serviceRepository.GetAll().Find(p => p.Name == name));
            IHubContext context = Startup.ConnectionManager.GetHubContext<ListenerHub>();
            context.Clients.All.messageAdded(message, type, index);
        }

        public void AddReport(string unitName, int companyId, int reportId, string message, string parameterString, string date, string name)
        {
            int index = _serviceRepository.GetAll().IndexOf(_serviceRepository.GetAll().Find(p => p.Name == name));
            IHubContext context = Startup.ConnectionManager.GetHubContext<ListenerHub>();
            context.Clients.All.reportAdded(unitName, companyId, reportId, message, parameterString, date, index);
        }

        public void GetAll(int listener)
        {
            Clients.Caller.messageAll(_serviceRepository.GetAll()[listener].Last.ToArray(), listener);
        }

        public void GetLimit()
        {
            Clients.Caller.setLimit(FixedQueue<IMessage>.Limit);
        }

        public int GetListenerCount()
        {
            return _serviceRepository.GetAll().Count;
        }

    }
}