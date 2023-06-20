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
    public class QuestCreatingCommand : CommandBase
    {
        private QuestCreatorViewModel _creatorViewModel;

        public QuestCreatingCommand(QuestCreatorViewModel questCreatorViewModel)
        {
            _creatorViewModel = questCreatorViewModel;
        }

        public override void Execute(object? parameter)
        {
            QuestModel questModel = new QuestModel() 
            {
                id = _creatorViewModel.idInput,
                Id = "Quest",
                QuestDescription = _creatorViewModel.QuestDescriptionInput,
                IncrementStatType = _creatorViewModel.SelectedStatType,
                IncrementAmount = _creatorViewModel.IncrementStatAmountInput,
                StackedNumber = 0,
                QuestAcceptedFlag = 0
            };

            _ = QuestCreation(questModel);

        }

        private async Task QuestCreation(QuestModel _model) 
        {
            try
            {
                AzureDBContext DBContext = new AzureDBContext();

                await DBContext.CreateQuest(_model);

                MessageBox.Show("Quest Creation Completed!", "Success Message!");

                _creatorViewModel.QuestDescriptionInput = string.Empty;
                _creatorViewModel.idInput = string.Empty;
                _creatorViewModel.IncrementStatAmountInput = 0.0f;
                _creatorViewModel.SelectedStatType = string.Empty;

            }
            catch (CosmosException CosmosEx)
            {
                MessageBoxResult res = MessageBox.Show(CosmosEx.Message + "\n Click Ok to Retry or Cancel to Exit Application!", "Error", MessageBoxButton.OKCancel);

                if (res == MessageBoxResult.OK)
                {
                    _ = QuestCreation(_model);
                }
                else if (res == MessageBoxResult.Cancel)
                {
                }
            }
            catch (Exception ex)
            {
                MessageBoxResult res = MessageBox.Show(ex.Message + "\n Click Ok to Retry or Cancel to Exit Application!", "Error", MessageBoxButton.OKCancel);

                if (res == MessageBoxResult.OK)
                {
                    _ = QuestCreation(_model);
                }
                else if (res == MessageBoxResult.Cancel)
                {
                }
            }
        }
    }
}
