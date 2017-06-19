using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using Microsoft.AspNetCore.SignalR;
using listeners.Services;
using listeners.Models;

namespace listeners
{
    public class UdpListener : IListener
    {
        private int type = 1;
        private DateTime lastAction;
        private int port;
        private Thread thread;
        private bool isListenerRunning = false;
        private string name = "UdpListener";
        private FixedQueue<IMessage> queue = new FixedQueue<IMessage>();
        public string Name { get { return name; } set { name = value; } }
        public FixedQueue<IMessage> Last { get { return queue; } }
        public int Port { get { return port; } }
        public int Type { get { return type; } set { type = value; } }
        public DateTime LastAction { get { return lastAction; } set { lastAction = value; } }

        public UdpListener(int Port)
        {
            port = Port;
            Start();
        }

        public void Start()
        {
            thread = new Thread(Listen);
            thread.Start();
        }

        public void Stop()
        {
            isListenerRunning = false;
        }
        // tpl dataflow
        UdpClient receivingUdpClient;
        private void Listen()
        {
            isListenerRunning = true;
            receivingUdpClient = new UdpClient(port);
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

            while (isListenerRunning)
            {


                var res = receivingUdpClient.ReceiveAsync();
                res.Wait();
                var result = Processing(res.Result);
                
    

            }
        }
        private async Task<bool> Processing (UdpReceiveResult result)
        {
            var task = new Task<bool>(() => ProcessingSync(result));
            task.Start();
            return await task;
        }


        private bool ProcessingSync(UdpReceiveResult result)
        {
            Message newMessage = new Message(Encoding.ASCII.GetString(result.Buffer));
            Thread DoingSomething = new Thread(DoSomething);
            DoingSomething.Start(newMessage);
            queue.Enqueue(newMessage);
            var mn = newMessage.message + " ok";
            receivingUdpClient.SendAsync(Encoding.ASCII.GetBytes(mn), mn.Length, result.RemoteEndPoint);

            IHubContext context = Startup.ConnectionManager.GetHubContext<ListenerHub>();
            context.Clients.All.addMessage(newMessage.message, newMessage.type, name);
            return true;
        }

        private void DoSomething(object data)
        {
            Thread.Sleep(5000000);
        }

        public void Log()
        {

        }
    }
}
