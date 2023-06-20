using DataModelLayer.DataModels;

namespace HumanPlayerStatus.nUnitTest
{
    public class CosmosDbConnectivityTest
    {
        private AzureCosmosDatabaseAccess.AzureDBContext? _context { get; set; } = null;
        private PlayerStats _playerStats { get; set; }


        [SetUp]
        public void Setup()
        {
            _context = new AzureCosmosDatabaseAccess.AzureDBContext();
            _playerStats = new PlayerStats()
            {
                id = "HumanPlayerData|Meian|Stats",
                Id = "Meian",
                HumanPlayerLevel = 1,
                HumanPlayerExp = 0,
                HumanPlayerLevelMaxExp = 100,
                STR=0,
                AGI=0,
                INT=0,
                COMM = 0,
                MENSTB=0,
                CRT=0,
                HP=0,
                HYG=0,
                Traits = new string[] {
                    "Question-Everything",
                    "Remember-Everything->Forget-Nothing",
                    "Observe->Observe-More",
                    "Think->Act",
                    "Logical->Logic->Logical-Analysis",
                    "Physical-Strength",
                    "Unlimited-Mental-Strength",
                    "Emotions->Tools",
                    "Never-Panic",
                    "Forever-Calm",
                    "Mathematical",
                    "Grind-Work",
                    "Guts",
                    "Optimize",
                    "Go-through-all-possible-scenarios",
                    "Be-prepared-for-everything",
                    "Never-Give-Up",
                    "Hold-Integrity"
                },
                Debuffs = new string[] 
                {
                    "Porn-Addiction",
                    "Extreme-Gaming",
                    "OverEating",
                    "Procastination",
                    "Youtube/Media-Addiction",
                    "Binge-Reading-Manga/Manhwa-Addiction",
                    "Anime-Binge-Watching-Addiction",
                    "Extensive-Depressed-Sleeping"
                }
            };
        }

        [Test]
        public async Task DbConnectivityCheck()
        {
            await _context?.UploadPlayerStats(_playerStats);

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