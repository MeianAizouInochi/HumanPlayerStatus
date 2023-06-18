using AzureCosmosDatabaseAccess;
using DataModelLayer.DataModels;
using HumanPlayerStatusApp.Commands;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HumanPlayerStatusApp.ViewModel
{
    public class QuestsViewModel:ViewModelBase
    {
		private ObservableCollection<QuestListItemViewModel> questList;

		public ObservableCollection<QuestListItemViewModel> QuestList
		{
			get { return questList; }
			set 
            { 
                questList = value;
                OnPropertyChanged(nameof(QuestList));
            }
		}

        public QuestsViewModel()
        {
            _ = MakeHumanPlayerQuestReadAPICall();
        }

        private async Task MakeHumanPlayerQuestReadAPICall()
        {
            QuestList = new ObservableCollection<QuestListItemViewModel>();

            //TODO: Create Functionality to Rerun this function on failure.
            try
            {
                AzureDBContext _dbContext = new AzureDBContext();

                List<QuestModel> Quests = await _dbContext.GetItemsInQuestContainer();

                foreach (QuestModel Q in Quests)
                {
                    QuestList.Add(new QuestListItemViewModel(Q)
                    {
                        QuestDescription = Q.QuestDescription,
                        IncrementAmountDetails = Q.IncrementAmount.ToString() + " " + Q.IncrementStatType,
                        StackedNumber = Q.StackedNumber,
                        QuestAcceptedFlag = Q.QuestAcceptedFlag,
                        AcceptButtonLabel = "Accept Quest",
                        DeclineButtonLabel = "Decline Quest",
                        SubmitButtonLabel = "Submit Quest"
                    });
                }
            }
            catch (CosmosException CosmosEx)
            {
                MessageBox.Show(CosmosEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
