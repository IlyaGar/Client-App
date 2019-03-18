using System.Windows;
using WpfClient.Services;
using WpfClient.ViewModels;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Service service = new Service();
            DataContext = new AppViewModel(service);
        }
    }
}
