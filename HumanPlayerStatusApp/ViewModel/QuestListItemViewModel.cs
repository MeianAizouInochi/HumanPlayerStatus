using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanPlayerStatusApp.ViewModel
{
    public class QuestListItemViewModel:ViewModelBase
    {
        private string? questDescription;

        public string? QuestDescription
        {
            get { return questDescription; }
            set 
            { 
                questDescription = value;
                OnPropertyChanged(nameof(QuestDescription));
            }
        }

        private string? imagePath;

        public string? ImagePath
        {
            get { return imagePath; }
            set 
            { 
                imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        private double incrementAmount;

        public double IncrementAmount
        {
            get { return incrementAmount; }
            set 
            { 
                incrementAmount = value;
                OnPropertyChanged(nameof(IncrementAmount));
            }
        }

        private string? incrementStatType;

        public string? IncrementStatType
        {
            get { return incrementStatType; }
            set 
            { 
                incrementStatType = value;
                OnPropertyChanged(nameof(IncrementStatType));
            }
        }

        private string? stackedLabel;

        public string? StackedLabel
        {
            get { return stackedLabel; }
            set 
            { 
                stackedLabel = value;
                OnPropertyChanged(nameof(StackedLabel));
            }
        }

        private int stackedNumber;

        public int StackedNumber
        {
            get { return stackedNumber; }
            set 
            { 
                stackedNumber = value;
                OnPropertyChanged(nameof(StackedNumber));
            }
        }

        
    }
}
