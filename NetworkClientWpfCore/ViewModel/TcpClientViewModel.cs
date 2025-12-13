using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetworkClientWpfCore.ViewModel
{
    public class TcpClientViewModel : ObserveableObject
    {
        public RelayCommand SendMessageCommand { private get; set; }
        public RelayCommand ConnectCommand { private get; set; }

        private string _msg = "";
        private string _receiver = "";
        private bool _connected = false;

        public bool Connected
        {
            set
            {
                _connected = value;
                OnPropertyChanged();
            }
            get
            {
                return _connected;
            }
        }

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

        public TcpClientViewModel()
        {
            SendMessageCommand = new RelayCommand((o) =>
                {
                    MessageBox.Show("Not implemented");
                },
                (o) =>
                {
                    return Connected;
                }
            );

            ConnectCommand = new RelayCommand((o) =>
            {
                MessageBox.Show("connect");
                Connected = true;
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
