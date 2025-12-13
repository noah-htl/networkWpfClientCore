using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NetworkClientWpfCore.Model;

namespace NetworkClientWpfCore.ViewModel
{
    public class TcpServerViewModel : ObserveableObject
    {
        private readonly ObservableCollection<ReceivedMessageModel> _receivedMessages;
        public IEnumerable<ReceivedMessageModel> ReceivedMessages => _receivedMessages;

        private int _bindPort;
        private IPAddress _bindIp;
        private string _rawBind = "";
        private string _state = "Stopped";

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

        public TcpServerViewModel()
        {
            _receivedMessages = new ObservableCollection<ReceivedMessageModel>();

            BindCommand = new RelayCommand(
                o =>
                {
                    MessageBox.Show("Not Implemented");
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
                    if (port < 1)
                    {
                        return false;
                    }

                    _bindIp = ip;
                    _bindPort = port;

                    return true;
                }
            );
        }
    }
}
