using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkClientWpfCore.Network
{
    internal class UDPServer
    {
        private Action<string, IPEndPoint> _handler;
        private Socket? _serverSocket;
        public UDPServer(Action<string, IPEndPoint> handler)
        {
            _handler = handler;
        }

        public void Listen(int port)
        {
            if(_serverSocket != null)
            {
                _serverSocket.Dispose();
            }

            _serverSocket = new Socket(SocketType.Dgram, ProtocolType.Udp);
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            _serverSocket.BeginAccept(AcceptCallback, null);
        }

        private void AcceptCallback(object o)
        {
            Console.WriteLine(o);
        }
    }
}
