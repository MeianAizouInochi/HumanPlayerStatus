using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HumanPlayerStatusApp.Commands
{
    public class CloseApplicationCommand : CommandBase
    {
        public CloseApplicationCommand() { }

        public override void Execute(object? parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
