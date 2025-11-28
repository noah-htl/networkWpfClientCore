using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkClientWpfCore.Model
{
    public class ReceivedMessageModel
    {
        public string Peer { private set; get; }
        public string Message { private set; get; }

        public ReceivedMessageModel(string peer, string message)
        {
            Peer = peer;
            Message = message;
        }
    }
}
