using AzureCosmosDatabaseAccess;
using DataModelLayer.DataModels;
using HumanPlayerStatusApp.Store;
using HumanPlayerStatusApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanPlayerStatusApp.Commands
{
    public class QuestSubmittionCommand : CommandBase
    {
        private readonly QuestModel _model;

        private readonly int _questAcceptedFlag;

        private readonly QuestListItemViewModel _questListItemViewModel;


        public QuestSubmittionCommand(QuestModel questModel, int QuestAcceptedFlag, QuestListItemViewModel questListItemViewModel)
        {
            _model = new QuestModel(questModel);

            _questAcceptedFlag = QuestAcceptedFlag;

            _questListItemViewModel = questListItemViewModel;
        }

        public override async void Execute(object? parameter)
        {
            _ = DbSubmitQuest();

        }

        private async Task DbSubmitQuest()
        {
            AzureDBContext azureDBContext = new AzureDBContext();

            _model.QuestAcceptedFlag = _questAcceptedFlag;

            _model.StackedNumber = _questListItemViewModel.StackedNumber + 1;

            await azureDBContext.UpdateQuest(_model);

            PlayerStats _HumanPlayer = await azureDBContext.GetPlayerStats(IDStore.UniqueId, IDStore.PartitionKey);

            switch (_model.IncrementStatType)
            {
                case "STR":
                    {
                        _HumanPlayer.STR += _model.IncrementAmount;

                        break;
                    }
                case "INT":
                    {
                        _HumanPlayer.INT += _model.IncrementAmount;

                        break;
                    }
                case "AGI":
                    {
                        _HumanPlayer.AGI += _model.IncrementAmount;

                        break;
                    }
                case "COMM":
                    {
                        _HumanPlayer.COMM += _model.IncrementAmount;

                        break;
                    }
                case "MENSTB":
                    {
                        _HumanPlayer.MENSTB += _model.IncrementAmount;

                        break;
                    }
                case "CRT":
                    {
                        _HumanPlayer.CRT += _model.IncrementAmount;

                        break;
                    }
                case "HP":
                    {
                        _HumanPlayer.HP += _model.IncrementAmount;

                        break;
                    }
                case "HYG":
                    {
                        _HumanPlayer.HYG += _model.IncrementAmount;

                        break;
                    }
            }

            _HumanPlayer.HumanPlayerExp += (long)(_model.IncrementAmount * 100);

            if (_HumanPlayer.HumanPlayerExp >= _HumanPlayer.HumanPlayerLevelMaxExp)
            {
                _HumanPlayer.HumanPlayerExp = _HumanPlayer.HumanPlayerExp - _HumanPlayer.HumanPlayerLevelMaxExp;

                _HumanPlayer.HumanPlayerLevel += 1;

                _HumanPlayer.HumanPlayerLevelMaxExp += _HumanPlayer.HumanPlayerLevel >= 25 ? (_HumanPlayer.HumanPlayerLevelMaxExp / 2) : (_HumanPlayer.HumanPlayerLevelMaxExp / 4);
            }

            await azureDBContext.UploadPlayerStats(_HumanPlayer);

            _questListItemViewModel.QuestAcceptedFlag = _questAcceptedFlag;

            _questListItemViewModel.StackedNumber = _model.StackedNumber;
        }
    }
}
