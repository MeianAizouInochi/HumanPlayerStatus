using HumanPlayerStatusApp.Store;
using HumanPlayerStatusApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanPlayerStatusApp.Commands
{
    public class NavigationCommand : CommandBase
    {
        private NavigationStore store;

        private int ID;

        public NavigationCommand(NavigationStore navStore, int NavigateToID)
        {

            this.store = navStore;
            
            ID = NavigateToID;
        }

        public override void Execute(object? parameter)
        {
            switch (ID)
            {
                case 0: 
                    {
                        store.CurrentViewModel = new DashboardViewModel();
                    }
                    break;
                case 1: 
                    {
                        store.CurrentViewModel = new QuestsViewModel();
                    }
                    break;
                default: 
                    {
                        store.CurrentViewModel = new DashboardViewModel();
                    }
                    break;
            }
        }
    }
}
