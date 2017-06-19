using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using listeners.Repositories;
using listeners.Services;


namespace listeners.Controllers
{
    public class ListenerController : Controller
    {

        private readonly IServiceRepository _serviceRepository;

        public ListenerController (IServiceRepository listenerRepository)
        {
            _serviceRepository = listenerRepository;
        }

        public IActionResult Index()
        {
            CreateListenersIfNoneExists();
            return View();
        }
        [HttpGet("/listeners")]
        public List<IService> GetListeners()
        {
            return _serviceRepository.GetAll();
        }
        private void CreateListenersIfNoneExists()
        {
            if (0 == _serviceRepository.GetAll().Count)
            {
                _serviceRepository.Add(new UdpListener(5757));
                _serviceRepository.Add(new TCPListener(5858));
            }
        }
    }
}