using AzureCosmosDatabaseAccess;
using DataModelLayer.DataModels;
using HumanPlayerStatusApp.ViewModel;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace HumanPlayerStatusApp.Commands
{
    public class QuestCreatingCommand : CommandBase
    {
        private NormalQuestCreatorViewModel _creatorViewModel;

        public QuestCreatingCommand(NormalQuestCreatorViewModel questCreatorViewModel)
        {
            _creatorViewModel = questCreatorViewModel;
        }

        public override void Execute(object? parameter)
        {
            QuestModel _model;

            if (_creatorViewModel is EventQuestCreatorViewModel)
            {
                _model = new EventQuestModel();

                InitializeQuestModelObject( _model );

                _model.QuestType = 1;

                EventQuestModel _model2 = (EventQuestModel)_model;

                _model2.StartDate = ((EventQuestCreatorViewModel)_creatorViewModel).StartDateArray;

                _model2.EndDate = ((EventQuestCreatorViewModel)_creatorViewModel).EndDateArray;

                if(DateTime.Today >= ((EventQuestCreatorViewModel)_creatorViewModel).StartDate)
                {
                    _model2.EventState = true;
                }
                else 
                {
                    _model2.EventState = false;
                }
                _model2.EventCompletionStatus = false;

            }
            else 
            {
                _model = new QuestModel();

                InitializeQuestModelObject( _model );

                _model.QuestType = 0;
            }

            _ = QuestCreation(_model);

        }

        private void InitializeQuestModelObject(QuestModel _model)
        {
            _model.id = _creatorViewModel.idInput;
            _model.Id = "Quest";
            _model.QuestDescription = _creatorViewModel.QuestDescriptionInput;
            _model.IncrementStatType = _creatorViewModel.SelectedStatType;
            _model.IncrementAmount = _creatorViewModel.IncrementStatAmountInput;
            _model.StackedNumber = 0;
            _model.QuestAcceptedFlag = 0;
        }

        private async Task QuestCreation(QuestModel _model) 
        {
            try
            {
                AzureDBContext DBContext = new AzureDBContext();

                await DBContext.CreateQuest(_model);

                MessageBox.Show("Quest Creation Completed!", "Success Message!");

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
