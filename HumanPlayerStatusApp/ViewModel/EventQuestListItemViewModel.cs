using System.Windows.Input;

namespace HumanPlayerStatusApp.ViewModel
{
    public class EventQuestListItemViewModel:ViewModelBase
    {
        public ICommand? QuestCompletion { get; set; }

        public string? QuestSubmissionButtonLabel { get; set; }
    }
}