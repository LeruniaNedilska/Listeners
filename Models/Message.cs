using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using listeners.Models;

namespace listeners
{
    public class Message: IMessage
    {
        public enum MessageType { Normal, Warning };
        public string message { get; set; }
        public MessageType type { get; set; }

    public Message(string message)
        {
            this.message = message;
            Random rnd = new Random();
            type = (MessageType)rnd.Next(0, 2);
        }
        public Message(string message, bool iftype)
        {
            if (iftype)
            {
                this.message = message;
                type = 0;
            }
        }
    }
}
