using HumanPlayerStatusApp.ViewModel;
using System;
using System.Diagnostics;


namespace HumanPlayerStatusApp.Store
{
    public class NavigationStore
    {
        public event Action? CurrentViewModelChanged;

		private ViewModelBase? currentViewModel;

		public ViewModelBase? CurrentViewModel
		{
			get { return currentViewModel; }
			set 
			{
				currentViewModel = value;

				OnCurrentViewModelChanged();
			}
		}

        private void OnCurrentViewModelChanged()
        {
			CurrentViewModelChanged?.Invoke();
        }

        public NavigationStore()
        {
            Debug.WriteLine("Created navigation store object.");
        }

        ~NavigationStore()
        {
            Debug.WriteLine("desroyed navigation store object.");
        }
    }
}
