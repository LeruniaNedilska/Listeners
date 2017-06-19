using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace listeners
{
    public class FixedQueue<IMessage>
    {
        ConcurrentQueue<IMessage> queue = new ConcurrentQueue<IMessage>();

        public static int Limit = 10;
        public void Enqueue(IMessage message)
        {
            queue.Enqueue(message);
            IMessage overflow;
            if (queue.Count > Limit) queue.TryDequeue(out overflow) ;
        }

        public IMessage[] ToArray()
        {
            return queue.ToArray();
        }
    }
}
