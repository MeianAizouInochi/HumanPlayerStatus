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
    public class QuestAcceptingCommand : CommandBase
    {
        private readonly QuestModel _model;

        private readonly int _questAcceptedFlag;

        private readonly QuestListItemViewModel _questListItemViewModel;

        public QuestAcceptingCommand(QuestModel Prev_Quest, int QuestAcceptedFlag, QuestListItemViewModel questListItemViewModel)
        {
            _model = new QuestModel(Prev_Quest);

            _questAcceptedFlag = QuestAcceptedFlag;
            
            _questListItemViewModel = questListItemViewModel;
        }

        //TODO: Handle Exceptions here
        public override void Execute(object? parameter)
        {
            _ = DbAcceptQuest();
        }

        private async Task DbAcceptQuest() 
        {
            AzureDBContext DBContext = new AzureDBContext();

            _model.QuestAcceptedFlag = _questAcceptedFlag;

            await DBContext.UpdateQuest(_model);

            _questListItemViewModel.QuestAcceptedFlag = _questAcceptedFlag;
            
        }
    }
}
