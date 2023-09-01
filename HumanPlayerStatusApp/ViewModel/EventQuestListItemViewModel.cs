using DataModelLayer.DataModels;
using HumanPlayerStatusApp.Commands;
using System.Windows.Input;

namespace HumanPlayerStatusApp.ViewModel
{
    public class EventQuestListItemViewModel :QuestListItemViewModel
    {
        public string StartDateLabel { get; set; }

        private string startDate;

        public string StartDate
        {
            get { return startDate; }
            set 
            { 
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public string EndDateLabel { get; set; }

        private string endDate;

        public string EndDate
        {
            get { return endDate; }
            set 
            { 
                endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public string EventCompletionStatusLabel { get; set; }

        private string eventCompletionStatus;

        public string EventCompletionStatus
        {
            get { return eventCompletionStatus; }
            set 
            { 
                eventCompletionStatus = value;
                OnPropertyChanged(nameof(EventCompletionStatus));
            }
        }

        public string EventStateLabel { get; set; }

        private string eventState;

        public string EventState
        {
            get { return eventState; }
            set 
            { 
                eventState = value;
                OnPropertyChanged(nameof(EventState));
            }
        }

        public EventQuestListItemViewModel()
        {
            
        }

        public EventQuestListItemViewModel(EventQuestModel eventquestModel)
        {
            AcceptQuest = new QuestAcceptingCommand(eventquestModel, 1, this);

            DeclineQuest = new QuestDecliningCommand(eventquestModel, 0, this);

            SubmitQuest = new QuestSubmittionCommand(eventquestModel, 0, this);
        }
    }
}