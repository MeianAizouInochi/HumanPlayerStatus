using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanPlayerStatusApp.ViewModel
{
    /// <summary>
    /// Base class for the View Models. It implements the INotifyPropertyChanged interface.
    /// This Class is Mother of all ViewModel Classes, for them to be able to use Property Changed Event.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// This Method should be called when updating the Data in the UI, for it o be rendered.
        /// </summary>
        /// <param name="PropertyName"></param>
        protected void OnPropertyChanged(string? PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
