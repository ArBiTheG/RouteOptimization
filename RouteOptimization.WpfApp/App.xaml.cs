using RouteOptimization.WpfApp.Core;
using RouteOptimization.WpfApp.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace RouteOptimization.WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore navigationStore = new NavigationStore();

            MainWindow = new MainWindow()
            {
                DataContext = new MainVM(navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}
