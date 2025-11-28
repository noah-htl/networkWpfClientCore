using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Threading;

namespace NetworkClientWpfCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Dispatcher Dispatcher { get; private set; }

        public App()
        {
            Dispatcher = Dispatcher.CurrentDispatcher;
        }
    }
}
