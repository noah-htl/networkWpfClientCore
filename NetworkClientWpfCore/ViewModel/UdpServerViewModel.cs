using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using NetworkClientWpfCore.Model;
using NetworkClientWpfCore.Network;

namespace NetworkClientWpfCore.ViewModel
{
    public class UdpServerViewModel : ObserveableObject
    {
        private readonly ObservableCollection<ReceivedMessageModel> _receivedMessages;
        private int _bindPort;

        private UdpClient? _udpClient;

        public IEnumerable<ReceivedMessageModel> ReceivedMessages => _receivedMessages;
        public int BindPort
        {
            get => _bindPort;
            set
            {
                _bindPort = value;
                OnPropertyChanged(nameof(BindPort));
            }
        }

        public ICommand BindCommand { get; set; }

        public UdpServerViewModel()
        {
            _receivedMessages = new ObservableCollection<ReceivedMessageModel>();
            _udpClient = null;

            BindCommand = new RelayCommand(
                o =>
                    {
                        if (_udpClient != null)
                        {
                            _udpClient.Dispose();
                        }

                        _udpClient = new UdpClient(BindPort);
                        _udpClient.BeginReceive(OnReceive, null);
                    },
                o =>
                    {
                        return BindPort > 0 && BindPort < 65535;
                    }
            );
        }

        private void OnReceive(IAsyncResult o)
        {
            if(_udpClient == null) return; // should never happen

            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            byte[] receivedBytes = _udpClient.EndReceive(o, ref ep);
            string receivedText = Encoding.ASCII.GetString(receivedBytes);

            string peer = $"{ep.Address}:{ep.Port}";

            App.Dispatcher.Invoke(() =>
            {
                _receivedMessages.Add(new ReceivedMessageModel(peer, receivedText));
                OnPropertyChanged(nameof(ReceivedMessages));
            });

            _udpClient.BeginReceive(OnReceive, null);
        }
    }
}
