using AzureCosmosDatabaseAccess;
using DataModelLayer.DataModels;
using HumanPlayerStatusApp.Store;
using HumanPlayerStatusApp.ViewModel;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;
using System.Windows;

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

        public override void Execute(object? parameter)
        {
            _ = DbSubmitQuest();

        }

        //TODO: Create a Transaction here, or call a transaction from azuredbcontext here.
        private async Task DbSubmitQuest()
        {
            try 
            {
                //Update Quest
                AzureDBContext azureDBContext = new AzureDBContext();

                _model.QuestAcceptedFlag = _questAcceptedFlag;

                _model.StackedNumber = _questListItemViewModel.StackedNumber + 1;

                await azureDBContext.UpdateQuest(_model);

                PlayerStats _HumanPlayer = await azureDBContext.GetPlayerStats(IDStore.StatsUniqueId, IDStore.StatsPartitionKey);

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

                //read statshistory
                StatHistory _stathistory = await azureDBContext.GetStatHistory("HumanPlayerData|Meian|StatHistory", "Meian");

                //perform functionalities on the above object.
                switch (_model.IncrementStatType)
                {
                    case "STR":
                        {
                            DateTime dt = DateTime.Now;
                            _stathistory.STR_history = new int[] { dt.Day,dt.Month,dt.Year};

                            break;
                        }
                    case "INT":
                        {
                            DateTime dt = DateTime.Now;
                            _stathistory.INT_history = new int[] { dt.Day, dt.Month, dt.Year };

                            break;
                        }
                    case "AGI":
                        {
                            DateTime dt = DateTime.Now;
                            _stathistory.AGI_history = new int[] { dt.Day, dt.Month, dt.Year };

                            break;
                        }
                    case "COMM":
                        {
                            DateTime dt = DateTime.Now;
                            _stathistory.COMM_history = new int[] { dt.Day, dt.Month, dt.Year };

                            break;
                        }
                    case "MENSTB":
                        {
                            DateTime dt = DateTime.Now;
                            _stathistory.MENSTB_history = new int[] { dt.Day, dt.Month, dt.Year };

                            break;
                        }
                    case "CRT":
                        {
                            DateTime dt = DateTime.Now;
                            _stathistory.CRT_history = new int[] { dt.Day, dt.Month, dt.Year };

                            break;
                        }
                    case "HP":
                        {
                            DateTime dt = DateTime.Now;
                            _stathistory.HP_history = new int[] { dt.Day, dt.Month, dt.Year };

                            break;
                        }
                    case "HYG":
                        {
                            DateTime dt = DateTime.Now;
                            _stathistory.HYG_history = new int[] { dt.Day, dt.Month, dt.Year };

                            break;
                        }
                }

                //update stathistory
                await azureDBContext.UpateStatHistory(_stathistory);

                _questListItemViewModel.QuestAcceptedFlag = _questAcceptedFlag;

                _questListItemViewModel.StackedNumber = _model.StackedNumber;

            }
            catch (CosmosException CosmosEx) 
            {
                MessageBox.Show(CosmosEx.Message, "Some Major Shit happened!");
            }           
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message, "Some Major - Major Shit happened!");
            }
        }
    }
}
