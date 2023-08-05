using DataModelLayer.DataModels;

namespace HumanPlayerStatus.nUnitTest
{
    public class CosmosDbConnectivityTest
    {
        private AzureCosmosDatabaseAccess.AzureDBContext? _context { get; set; } = null;
        private StatHistory StatsHis { get; set; }


        [SetUp]
        public void Setup()
        {
            DateTime obj = DateTime.Now;
            int day = obj.Day;
            int year = obj.Year;
            int month = obj.Month;
            _context = new AzureCosmosDatabaseAccess.AzureDBContext();
            StatsHis = new StatHistory()
            {
                id = "HumanPlayerData|Meian|StatHistory",
                Id = "Meian",
                STR_history = new int[] { day,month,year },
                AGI_history = new int[] { day,month, year },
                INT_history = new int[] { day,month,year},
                COMM_history = new int[] { day,month,year},
                MENSTB_history = new int[] { day,month,year},
                CRT_history = new int[] { day,month,year},
                HP_history = new int[] { day,month,year},
                HYG_history = new int[] { day,month,year}

            };
        }

        [Test]
        public async Task DbConnectivityCheck()
        {
            await _context?.CreatestatHistory(StatsHis);

            //Assert.IsInstanceOf<PlayerStats>(obj);
        }

        [Test]
        public async Task QueryDataCosmos() 
        {
            List<QuestModel> Quests = await _context.GetItemsInQuestContainer();

            //Assert.That(Quests.Count, Is.EqualTo(2));
            Assert.That(Quests[0].id, Is.EqualTo("01"));

        }
    }
}