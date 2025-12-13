using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace NetworkClientWpfCore.ViewModel
{
    public class UdpClientViewModel : ObserveableObject
    {
        public RelayCommand SendMessageCommand { private get; set; }

        private string _msg = "";
        private string _receiver = "";

        public string Message
        {
            set
            {
                _msg = value;
                OnPropertyChanged();
            }
            get { return _msg; }
        }

        public string ReceiverAddress
        {
            set
            {
                _receiver = value;
                OnPropertyChanged();
            }

            get { return _receiver; }
        }

        public UdpClientViewModel()
        {
            SendMessageCommand = new RelayCommand((o) =>
            {
                int i = 0;
                string msg = Message;

                string ip = ReceiverAddress.Split(':')[0];
                int port = Convert.ToInt32(ReceiverAddress.Split(':')[1]);

                // ToDO send UDP
                var udp = new UdpClient();
                var data = Encoding.ASCII.GetBytes(msg + '\r');
                udp.Send(data, data.Length, ip, port);
            },
            (o) =>
            {
                string[] parts = ReceiverAddress.Split(":");
                IPAddress? ip;
                int port;
                if (parts.Length != 2)
                {
                    return false;
                }
                if (!IPAddress.TryParse(parts[0], out ip))
                {
                    return false;
                }
                if (!int.TryParse(parts[1], out port))
                {
                    return false;
                }
                if (port < 1)
                {
                    return false;
                }

                return true;
            });
        }
    }
}
