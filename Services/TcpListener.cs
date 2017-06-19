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
    public class TCPListener : IListener
    {
        private int type = 1;
        private DateTime lastAction;
        private int port;
        private Thread thread;
        private bool isListenerRunning = false;
        private TcpListener listener;
        private string name = "TcpListener";
        private FixedQueue<IMessage> queue = new FixedQueue<IMessage>();

        public int Port { get { return port; } }
        public string Name { get { return name; } set { name = value; } }
        public FixedQueue<IMessage> Last { get { return queue; } }
        public int Type { get { return type; } set { type = value; } }
        public DateTime LastAction { get { return lastAction; } set { lastAction = value; } }

        public TCPListener(int Port)
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

        private void Listen()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("Listen(): Error running TcpListener, Exception=" + e.Message);
                return;
            }
            isListenerRunning = true;

            while (isListenerRunning)
            {
                Task<TcpClient> client = listener.AcceptTcpClientAsync();
                string receivedString = null;
                Byte[] bytes = new Byte[256];

                NetworkStream stream = client.Result.GetStream();

                int i;

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    receivedString = Encoding.ASCII.GetString(bytes, 0, i);
                    Message newMessage = new Message(receivedString);
                    queue.Enqueue(newMessage);

                    IHubContext context = Startup.ConnectionManager.GetHubContext<ListenerHub>();
                    context.Clients.All.addMessage(newMessage.message, newMessage.type, name);
                    Console.WriteLine("Message received: " + receivedString);
                }
            }
        }

        public void Log()
        {

        }
    }
}
