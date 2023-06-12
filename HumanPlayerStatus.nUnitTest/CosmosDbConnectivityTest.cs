
using DataModelLayer;

namespace HumanPlayerStatus.nUnitTest
{
    public class CosmosDbConnectivityTest
    {
        private AzureCosmosDatabaseAccess.AzureDBContext? _context { get; set; } = null;
        private PlayerStats? _playerStats { get; set; } = null;


        [SetUp]
        public void Setup()
        {
            _context = new AzureCosmosDatabaseAccess.AzureDBContext();
            _playerStats = new PlayerStats() 
            {
                id="HumanPlayerStatusData|Meian",
                Id="Meian",
                STR=1,
                AGI=0,
                INT=0,
                COMM = 0,
                MENSTB=0,
                CRT=0,
                HP=0,
                HYG=0
            };
        }

        [Test]
        public async Task DbConnectivityCheck()
        {
            await _context.UploadPlayerStats(_playerStats);

            Assert.That(_context.ResponseCode, Is.EqualTo(200));
        }
    }
}