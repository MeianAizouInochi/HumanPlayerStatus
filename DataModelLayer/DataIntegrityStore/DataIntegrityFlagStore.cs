using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelLayer.DataIntegrityStore
{
    public class DataIntegrityFlagStore
    {
        public event Action? FlagChanged;

		private int flag;

		public int Flag
		{
			get { return flag; }
			set 
			{ 
				flag = value;

				OnFlagChanged();
			}
		}


		private void OnFlagChanged() 
		{
			FlagChanged?.Invoke();
		}

	}
}
