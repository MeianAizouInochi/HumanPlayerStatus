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

        //TODO: Create functionality to retry executing the function.
        private async Task DbDeclineQuest()
        {
            try
            {
                AzureDBContext azureDBContext = new AzureDBContext();

                questModel.QuestAcceptedFlag = questAcceptedFlag;

                await azureDBContext.UpdateQuest(questModel);

                questListItemViewModel.QuestAcceptedFlag = questAcceptedFlag;
            }
            catch (CosmosException CosmosEx)
            {
                MessageBoxResult res = MessageBox.Show(CosmosEx.Message + "\n Click Ok to Retry or Cancel to Exit Application!", "Error", MessageBoxButton.OKCancel);

                if (res == MessageBoxResult.OK)
                {
                    _ = DbDeclineQuest();
                }
                else if (res == MessageBoxResult.Cancel)
                {
                    Application.Current.Shutdown();
                }
            }
            catch (Exception ex)
            {
                MessageBoxResult res = MessageBox.Show(ex.Message + "\n Click Ok to Retry or Cancel to Exit Application!", "Error", MessageBoxButton.OKCancel);

                if (res == MessageBoxResult.OK)
                {
                    _ = DbDeclineQuest();
                }
                else if (res == MessageBoxResult.Cancel)
                {
                    Application.Current.Shutdown();
                }
            }
        }

    }
}
