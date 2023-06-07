using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HumanPlayerStatusApp.ViewModel
{
    public class NavigationMenuItemsList
    {
        public ICommand? NavigateToDashboardCommand { get; set; }

        public ICommand? NavigateToQuestCommand { get; set; }


    }
}
