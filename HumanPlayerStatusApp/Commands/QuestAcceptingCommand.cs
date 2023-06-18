using AzureCosmosDatabaseAccess;
using DataModelLayer.DataModels;
using HumanPlayerStatusApp.ViewModel;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public override void Execute(object? parameter)
        {
            _ = DbAcceptQuest();
        }

        //TODO: Create a functionality to retry execution.
        private async Task DbAcceptQuest() 
        {
            try
            {
                AzureDBContext DBContext = new AzureDBContext();

                _model.QuestAcceptedFlag = _questAcceptedFlag;

                await DBContext.UpdateQuest(_model);

                _questListItemViewModel.QuestAcceptedFlag = _questAcceptedFlag;
            }
            catch (CosmosException CosmosEx)
            {
                MessageBoxResult res =  MessageBox.Show(CosmosEx.Message+"\n Click Ok to Retry or Cancel to Exit Application!","Error",MessageBoxButton.OKCancel);

                if(res == MessageBoxResult.OK) 
                {
                    _ = DbAcceptQuest();
                }
                else if(res == MessageBoxResult.Cancel) 
                {
                    Application.Current.Shutdown();
                }
            }
            catch (Exception ex) 
            {
                MessageBoxResult res = MessageBox.Show(ex.Message + "\n Click Ok to Retry or Cancel to Exit Application!", "Error", MessageBoxButton.OKCancel);
                
                if(res == MessageBoxResult.OK)
                {
                    _ = DbAcceptQuest();
                }
                else if (res == MessageBoxResult.Cancel)
                {
                    Application.Current.Shutdown();
                }
            }

        }
    }
}
