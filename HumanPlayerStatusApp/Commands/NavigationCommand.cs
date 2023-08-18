using HumanPlayerStatusApp.Store;
using HumanPlayerStatusApp.ViewModel;

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

                case 2: 
                    {
                        store.CurrentViewModel = new QuestCreatorViewModel();
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
