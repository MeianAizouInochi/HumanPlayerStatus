using DataModelLayer.DataModels;

namespace HumanPlayerStatus.nUnitTest
{
    public class CosmosDbConnectivityTest
    {
        private AzureCosmosDatabaseAccess.AzureDBContext? _context { get; set; } = null;
        private EventQuestModel _quest { get; set; }


        [SetUp]
        public void Setup()
        {
            _context = new AzureCosmosDatabaseAccess.AzureDBContext();

            _quest = new EventQuestModel()
            {
                id = "HumanPlayerData|Meian|EventQuest|01",
                Id = "Quest",
                QuestDescription = "ABC",
                IncrementStatType = "INT",
                IncrementAmount = 0.1F,
                StackedNumber = 1,
                QuestAcceptedFlag = 0,
                QuestType = 1,
                StartDate = new int[] {2,2,3},
                EndDate = new int[] {1,2,4},
                EventCompletionStatus = false,
                EventState = true
            };
        }

        //Perform UpdateEventQuest Test. - Done
        //Perform Create event Quest Test. -  Done
        //Perform Accepting Event quest Test.
        //Perform Declining event Quest test.

        [Test]
        public async Task DbConnectivityCheck()
        {
            await _context?.UpdateQuest(_quest);

            //Assert.IsInstanceOf<PlayerStats>(obj);
        }
    }
}