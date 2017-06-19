using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using listeners.Models;

namespace listeners
{
    public interface IListenerHub
    {
        void messageAdded(string message, int type, string name);
        void reportAdded(string unitName, int companyId, int reportId, string message, string parameterString, string date, string name);
        void messageAll(IMessage[] messages, int listener);
        void setLimit(int limit);

    }
}
