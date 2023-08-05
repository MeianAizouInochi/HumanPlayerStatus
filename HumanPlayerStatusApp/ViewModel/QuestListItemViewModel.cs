using DataModelLayer.DataModels;
using HumanPlayerStatusApp.Commands;
using System.Windows.Input;

namespace HumanPlayerStatusApp.ViewModel
{
    public class QuestListItemViewModel:ViewModelBase
    {
        public ICommand? AcceptQuest { get; set; }

        public ICommand? DeclineQuest { get; set; }

        public ICommand? SubmitQuest { get; set; }

        public string? AcceptButtonLabel { get; set; }

        public string? DeclineButtonLabel { get; set; }

        public string? SubmitButtonLabel { get; set; }

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

        private string? incrementAmountDetails;

        public string? IncrementAmountDetails
        {
            get { return incrementAmountDetails; }
            set 
            {
                incrementAmountDetails = value;
                OnPropertyChanged(nameof(IncrementAmountDetails));
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

        private int questAcceptedFlag;

        public int QuestAcceptedFlag
        {
            get { return questAcceptedFlag; }
            set 
            { 
                questAcceptedFlag = value;
                OnPropertyChanged(nameof(QuestAcceptedFlag));
            }
        }

        public QuestListItemViewModel(QuestModel questModel)
        {
            AcceptQuest = new QuestAcceptingCommand(questModel, 1, this);

            DeclineQuest = new QuestDecliningCommand(questModel,0,this);

            SubmitQuest = new QuestSubmittionCommand(questModel, 0, this);
        }

    }
}
