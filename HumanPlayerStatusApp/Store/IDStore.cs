using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HumanPlayerStatusApp.Store
{
    public class IDStore
    {
        public static readonly string HumanPlayerDataContainerID = "HumanPlayerData";

        public static readonly string StatsUniqueId = "HumanPlayerData|Meian|Stats";

        public static readonly string StatsPartitionKey = "Meian";

        public static readonly string QuestPartitionKey = "Quest";
    }
}
