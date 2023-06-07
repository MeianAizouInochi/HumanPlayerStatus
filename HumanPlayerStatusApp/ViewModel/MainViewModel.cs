using HumanPlayerStatusApp.Commands;
using HumanPlayerStatusApp.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanPlayerStatusApp.ViewModel
{
    /// <summary>
    /// This is the ViewModel which is attached to the Main Window of the Application.
    /// All the internal UI elements are part of the MainWindow,
    /// which in turn also means all elements inside MainWindow can be part of the Main View Model class. 
    /// </summary>
    public class MainViewModel:ViewModelBase
    {
        public string WindowTitle { get; set; } = "STATUS";

        public string HumanPlayerNameLabel { get; set; }

        public string HumanPlayerName { get; set; }



        public NavigationMenuItemsList navigationMenuItemsList { get; set; }

        public NavigationStore navigationStore { get; set; }

        public ViewModelBase CurrentViewModel
        {
            get{ return navigationStore.CurrentViewModel;}
        }

        public MainViewModel(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;

            this.navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            HumanPlayerNameLabel = "Name: ";

            HumanPlayerName = "Meian";

            navigationMenuItemsList = new NavigationMenuItemsList() {
                NavigateToDashboardCommand = new NavigationCommand(navigationStore,0),
                NavigateToQuestCommand = new NavigationCommand(navigationStore,1)
            };
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
