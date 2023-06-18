using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelLayer.DataModels
{
    public class QuestModel
    {
        public string? id { get; set; }

        public string? Id { get; set; }
        
        public string? QuestDescription { get; set; }
        
        public string? IncrementStatType { get; set; }
        
        public double IncrementAmount { get; set; }
        
        public int StackedNumber { get; set; }

        public int QuestAcceptedFlag { get; set; }

        public QuestModel()
        {
            
        }

        public QuestModel( QuestModel obj)
        {
            id = obj.id;
            Id = obj.Id;
            QuestDescription = obj.QuestDescription;
            IncrementAmount = obj.IncrementAmount;
            IncrementStatType = obj.IncrementStatType;
            StackedNumber = obj.StackedNumber;
            QuestAcceptedFlag = obj.QuestAcceptedFlag;
        }
    }
}
