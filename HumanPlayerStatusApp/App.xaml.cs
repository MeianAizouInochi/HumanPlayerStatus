using HumanPlayerStatusApp.Store;
using HumanPlayerStatusApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HumanPlayerStatusApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            NavigationStore navStore = new NavigationStore() { CurrentViewModel = new DashboardViewModel() };

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navStore) { WindowTitle="STATUS"}
            };

            MainWindow.Show();
        }
    }
}
