using HumanPlayerStatusApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HumanPlayerStatusApp.ViewModel
{
    public class NormalQuestCreatorViewModel:ViewModelBase
    {

        public string? CreateQuestButtonLabel { get; set; }

        public string idInputLabel { get; set; }

        private string? _idInput;

        public string? idInput
        {
            get { return _idInput; }
            set
            {
                _idInput = value;
                OnPropertyChanged(nameof(idInput));
            }
        }

        public string QuestDescriptionInputLabel { get; set; }

        private string? questDescriptionInput;

        public string? QuestDescriptionInput
        {
            get { return questDescriptionInput; }
            set
            {
                questDescriptionInput = value;
                OnPropertyChanged(nameof(QuestDescriptionInput));
            }
        }

        public string IncrementStatTypeInputLabel { get; set; }

        private List<string>? statTypes;

        public List<string>? StatTypes
        {
            get { return statTypes; }
            set
            {
                statTypes = value;

                OnPropertyChanged(nameof(StatTypes));
            }
        }

        private string? selectedStatType;

        public string? SelectedStatType
        {
            get { return selectedStatType; }
            set
            {
                selectedStatType = value;
                OnPropertyChanged(nameof(SelectedStatType));
            }
        }

        public string IncrementStatAmountInputLabel { get; set; }

        private float incrementStatAmountInput;

        public float IncrementStatAmountInput
        {
            get { return incrementStatAmountInput; }
            set
            {
                incrementStatAmountInput = value > 0 ? value >= 1 ? 1 : value : 0.1f;

                OnPropertyChanged(nameof(IncrementStatAmountInput));
            }
        }



        public ICommand? CreateQuest { get; }



        public NormalQuestCreatorViewModel()
        {
            idInputLabel = "Provide Quest Unique id:";

            QuestDescriptionInputLabel = "Provide Quest Description and specifics:";

            IncrementStatTypeInputLabel = "Provide which stat will be incremented on quest completion:";

            IncrementStatAmountInputLabel = "Provide how much amount the stat should increase: Range[0<x<=1]";

            StatTypes = new List<string>()
            {
                "STR",
                "AGI",
                "INT",
                "COMM",
                "MENSTB",
                "CRT",
                "HP",
                "HYG"
            };

            CreateQuestButtonLabel = "Create Quest";

            CreateQuest = new QuestCreatingCommand(this);
        }
    }
}
