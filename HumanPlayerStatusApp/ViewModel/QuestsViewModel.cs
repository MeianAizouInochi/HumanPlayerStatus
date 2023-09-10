using AzureCosmosDatabaseAccess;
using DataModelLayer.DataModels;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace HumanPlayerStatusApp.ViewModel
{
    public class QuestsViewModel:ViewModelBase
    {
        private ObservableCollection<EventQuestListItemViewModel>? eventQuestList;

        public ObservableCollection<EventQuestListItemViewModel>? EventQuestList
        {
            get { return eventQuestList; }
            set 
            { 
                eventQuestList = value;

                OnPropertyChanged(nameof(eventQuestList));
            }
        }


        private ObservableCollection<QuestListItemViewModel>? questList;

		public ObservableCollection<QuestListItemViewModel>? QuestList
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
            _ = MakeHumanPlayerQuestReadAPICall(); // Modify this to make calls for both normal and event quest.

            _ = MakeHumanPlayerEventQuestReadAPICall();
        }

        private async Task MakeHumanPlayerEventQuestReadAPICall()
        {
            EventQuestList = new ObservableCollection<EventQuestListItemViewModel>();

            try
            {
                AzureDBContext _dbContext = new AzureDBContext();

                List<EventQuestModel> EventQuests = await _dbContext.GetItemsInEventQuestContainer();

                foreach (EventQuestModel Q in EventQuests)
                {
                    EventQuestList.Add(new EventQuestListItemViewModel(Q)
                    {
                        QuestDescription = Q.QuestDescription,
                        IncrementAmountDetails = Q.IncrementAmount.ToString() + " " + Q.IncrementStatType,
                        StackedNumber = Q.StackedNumber,
                        QuestAcceptedFlag = Q.QuestAcceptedFlag,
                        AcceptButtonLabel = "Accept Quest",
                        DeclineButtonLabel = "Decline Quest",
                        SubmitButtonLabel = "Submit Quest",
                        StartDate = (new DateTime(Q.StartDate[2], Q.StartDate[1], Q.StartDate[0])).ToString("dd/MM/yyyy"),
                        EndDate = (new DateTime(Q.EndDate[2], Q.EndDate[1], Q.EndDate[0])).ToString("dd/MM/yyyy"),
                        EventCompletionStatus = Q.EventCompletionStatus ? "Completed" : "Not Completed",
                        EventState = Q.EventState ? "OnGoging" : "Ended"
                    });
                }
            }
            catch (CosmosException CosmosEx)
            {
                MessageBoxResult res = MessageBox.Show(CosmosEx.Message + "\n Click Ok to Retry or Cancel to Exit Application!", "Error", MessageBoxButton.OKCancel);

                if (res == MessageBoxResult.OK)
                {
                    _ = MakeHumanPlayerEventQuestReadAPICall();
                }
                else if (res == MessageBoxResult.Cancel)
                {
                    Application.Current.Shutdown();
                }
                MessageBox.Show(CosmosEx.Message);
            }
            catch (Exception ex)
            {

                MessageBoxResult res = MessageBox.Show(ex.Message + "\nClick Ok to Retry or Cancel to Exit Application!", "Error", MessageBoxButton.OKCancel);

                if (res == MessageBoxResult.OK)
                {
                    _ = MakeHumanPlayerEventQuestReadAPICall();
                }
                else if (res == MessageBoxResult.Cancel)
                {
                    Application.Current.Shutdown();
                }
            }
        }

        private async Task MakeHumanPlayerQuestReadAPICall()
        {
            QuestList = new ObservableCollection<QuestListItemViewModel>();

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
                MessageBoxResult res = MessageBox.Show(CosmosEx.Message + "\n Click Ok to Retry or Cancel to Exit Application!", "Error", MessageBoxButton.OKCancel);

                if (res == MessageBoxResult.OK)
                {
                    _ = MakeHumanPlayerQuestReadAPICall();
                }
                else if (res == MessageBoxResult.Cancel)
                {
                    Application.Current.Shutdown();
                }
                MessageBox.Show(CosmosEx.Message);
            }
            catch (Exception ex)
            {
                MessageBoxResult res = MessageBox.Show(ex.Message + "\n Click Ok to Retry or Cancel to Exit Application!", "Error", MessageBoxButton.OKCancel);

                if (res == MessageBoxResult.OK)
                {
                    _ = MakeHumanPlayerQuestReadAPICall();
                }
                else if (res == MessageBoxResult.Cancel)
                {
                    Application.Current.Shutdown();
                }
            }
        }
    }
}
