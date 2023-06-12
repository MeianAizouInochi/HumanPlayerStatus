using DataModelLayer;
using HumanPlayerStatusApp.Store;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace AzureCosmosDatabaseAccess
{
    public class AzureDBContext
    {
        private const string DatabaseId = "HumanPlayerStatusAppDatabase";

        private const string ContainerId = "HumanPlayerStatusData";

        private CosmosClient _Client;

        public int ResponseCode { get; set; }

        public AzureDBContext()
        {
            _Client = new(
                accountEndpoint: "",
                authKeyOrResourceToken: ""
                ) ;
        }

        public async Task UploadPlayerStats(PlayerStats playerstats, DataIntegrityFlagStore? Flag = null) 
        {
            Database DB = _Client.GetDatabase(DatabaseId);

            Container container = DB.GetContainer(ContainerId);

            ItemResponse<PlayerStats> Response = await container.UpsertItemAsync(item:playerstats,partitionKey:new PartitionKey("Meian"));

            ResponseCode = (int)Response.StatusCode;

        }
    }
}
