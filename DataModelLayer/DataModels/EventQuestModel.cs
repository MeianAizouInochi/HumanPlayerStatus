

namespace DataModelLayer.DataModels
{
    public class EventQuestModel:QuestModel
    {

        public int[] StartDate { get; set; }

        public int[] EndDate { get; set; }

        public bool EventCompletionStatus { get; set; }

        public bool EventState { get; set; }

        public EventQuestModel() { }


        public EventQuestModel( EventQuestModel obj)
        {
            id = obj.id;
            Id = obj.Id;
            QuestDescription = obj.QuestDescription;
            IncrementStatType = obj.IncrementStatType;
            IncrementAmount = obj.IncrementAmount;
            StackedNumber = obj.StackedNumber;
            QuestAcceptedFlag = obj.QuestAcceptedFlag;
            QuestType = obj.QuestType;
            StartDate  = obj.StartDate;
            EndDate = obj.EndDate;
            EventCompletionStatus = obj.EventCompletionStatus; 
            EventState = obj.EventState;
        }


    }
}
