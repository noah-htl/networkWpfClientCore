using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetworkClientWpfCore.ViewModel
{
    public class MainViewModel : ObserveableObject
    {
        public HomeViewModel HomeVM { get; private set; }

        public UdpClientViewModel UdpClientVM { get; private set; }
        public UdpServerViewModel UdpServerVM { get; private set; }
        public TcpClientViewModel TcpClientVM { get; private set; }
        public TcpServerViewModel TcpServerVM { get; private set; }
        public HttpServerViewModel HttpServerVM { get; private set; }

        public RelayCommand HomeViewCommand { get; set; }

        public RelayCommand UdpClientViewCommand { get; set; }
        public RelayCommand UdpServerViewCommand { get; set; }
        public RelayCommand TcpClientViewCommand { get; set; }
        public RelayCommand TcpServerViewCommand { get; set; }
        public RelayCommand HttpServerViewCommand { get; set; }

        public RelayCommand CloseAppCommand { get; set; }
 

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            UdpClientVM = new UdpClientViewModel();
            UdpServerVM = new UdpServerViewModel();
            TcpClientVM = new TcpClientViewModel();
            TcpServerVM = new TcpServerViewModel();
            HttpServerVM = new HttpServerViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            UdpClientViewCommand = new RelayCommand(o =>
            {
                CurrentView = UdpClientVM;
            });

            UdpServerViewCommand = new RelayCommand(o =>
            {
                CurrentView = UdpServerVM;
            });

            TcpClientViewCommand = new RelayCommand(o =>
            {
                CurrentView = TcpClientVM;
            });

            TcpServerViewCommand = new RelayCommand(o =>
            {
                CurrentView = TcpServerVM;
            });

            HttpServerViewCommand = new RelayCommand(o =>
            {
                CurrentView = HttpServerVM;
            });


            CloseAppCommand = new RelayCommand(o =>
            {
                Application.Current.Shutdown();
            });
        }
    }
}
