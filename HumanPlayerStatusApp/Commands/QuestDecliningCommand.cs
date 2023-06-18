using AzureCosmosDatabaseAccess;
using DataModelLayer.DataModels;
using HumanPlayerStatusApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanPlayerStatusApp.Commands
{
    public class QuestDecliningCommand : CommandBase
    {
        private readonly QuestModel questModel;

        private readonly int questAcceptedFlag;

        private readonly QuestListItemViewModel questListItemViewModel;

        public QuestDecliningCommand(QuestModel _questModel, int _QuestAcceptedFlag, QuestListItemViewModel _questListItemViewModel)
        {
            questModel = new QuestModel(_questModel);

            questAcceptedFlag = _QuestAcceptedFlag;

            questListItemViewModel = _questListItemViewModel;
        }

        public override async void Execute(object? parameter)
        {
            _ = DbDeclineQuest();
        }

        private async Task DbDeclineQuest()
        {
            AzureDBContext azureDBContext = new AzureDBContext();

            questModel.QuestAcceptedFlag = questAcceptedFlag;

            await azureDBContext.UpdateQuest(questModel);

            questListItemViewModel.QuestAcceptedFlag = questAcceptedFlag;
        }
    }
}
