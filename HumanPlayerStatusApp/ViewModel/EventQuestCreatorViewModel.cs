using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanPlayerStatusApp.ViewModel
{
    public class EventQuestCreatorViewModel:NormalQuestCreatorViewModel
    {

        public string StartDateLabel { get; set; }

        private DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set 
            { 
                startDate = value;

                OnPropertyChanged(nameof(StartDate));

                StartDateArray = new int[] { startDate.Day, startDate.Month, startDate.Year};
            }
        }

        public int[] StartDateArray { get; set; }

        public string EndDateLabel { get; set; }

        private DateTime endDate;

        public DateTime EndDate
        {
            get { return endDate; }
            set 
            { 
                endDate = value;

                OnPropertyChanged(nameof(EndDate));

                EndDateArray = new int[] { endDate.Day, endDate.Month, endDate.Year};
            }
        }

        public int[] EndDateArray { get; set; }


        public EventQuestCreatorViewModel()
        {
            StartDateLabel = "Event Starting Date:";

            EndDateLabel = "Event Ending Date:";
        }
    }
}
