using HumanPlayerStatusApp.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HumanPlayerStatusApp.ViewModel
{
    public class QuestCreatorViewModel:ViewModelBase
    {
        private NormalQuestCreatorViewModel normalQuestCreatorEntity;

        public NormalQuestCreatorViewModel NormalQuestCreatorEntity
        {
            get { return normalQuestCreatorEntity; }
            set { normalQuestCreatorEntity = value; }
        }

        private EventQuestCreatorViewModel eventQuestCreatorEntity;

        public EventQuestCreatorViewModel EventQuestCreatorEntity
        {
            get { return eventQuestCreatorEntity; }
            set { eventQuestCreatorEntity = value; }
        }


        
        public QuestCreatorViewModel()
        {
            

        }
    }
}

