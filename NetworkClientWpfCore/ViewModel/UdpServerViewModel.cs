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
        private IPAddress _bindIp;
        private string _rawBind = "";
        private string _state = "Stopped";

        private UdpClient? _udpClient;

        public IEnumerable<ReceivedMessageModel> ReceivedMessages => _receivedMessages;
        public string Bind
        {
            get => _rawBind;
            set
            {
                _rawBind = value;
                OnPropertyChanged(nameof(Bind));
            }
        }

        public string State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged(nameof(State));
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
                            State = "Stopped";
                        }

                        _udpClient = new UdpClient();
                        _udpClient.Client.Bind(new IPEndPoint(_bindIp, _bindPort));
                        State = $"Listening: {_bindIp}:{_bindPort}";
                        _udpClient.BeginReceive(OnReceive, null);
                    },
                o =>
                    {
                        string[] parts = Bind.Split(":");
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
                        if(port < 1025)
                        {
                            return false;
                        }

                        _bindIp = ip;
                        _bindPort = port;

                        return true;
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
