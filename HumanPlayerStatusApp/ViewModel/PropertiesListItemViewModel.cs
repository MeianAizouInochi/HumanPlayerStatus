using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanPlayerStatusApp.ViewModel
{
    public class PropertiesListItemViewModel:ViewModelBase
    {
		private string? propertyName;

		public string? PropertyName
		{
			get { return propertyName; }
			set 
			{ 
				propertyName = value;
				OnPropertyChanged(nameof(PropertyName));
			}
		}

		private float propertyValue;

		public float PropertyValue
		{
			get { return propertyValue; }
			set 
			{ 
				propertyValue = value;
				OnPropertyChanged(nameof(PropertyValue));
			}
		}

		public event Action? PropertyValueChanged;

    }
}
