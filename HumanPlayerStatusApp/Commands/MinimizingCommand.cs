using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HumanPlayerStatusApp.Commands
{
    public class MinimizingCommand : CommandBase
    {
        public MinimizingCommand()
        {
            
        }


        public override void Execute(object? parameter)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
    }
}
