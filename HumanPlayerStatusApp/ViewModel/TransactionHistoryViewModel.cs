using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanPlayerStatusApp.ViewModel
{
    public class TransactionHistoryViewModel:ViewModelBase
    {
        public ObservableCollection<TransactionRecordListItemViewModel>? TransactionRecordList { get; set; }


        public TransactionHistoryViewModel() 
        {

        }
    }
}
