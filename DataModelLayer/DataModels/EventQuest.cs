using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelLayer.DataModels
{
    public class EventQuest:QuestModel
    {
        public bool EventCompletionFlag { get; set; }



        public EventQuest()
        {
            
        }


        public EventQuest( EventQuest Obj)
        {
            id = Obj.id;
            Id = Obj.Id;
            EventCompletionFlag = Obj.EventCompletionFlag;
        }
    }
}
