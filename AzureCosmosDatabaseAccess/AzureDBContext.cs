using DataModelLayer.DataModels;
using DataModelLayer.DataIntegrityStore;
using HumanPlayerStatusExceptions;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using System.Windows;

namespace AzureCosmosDatabaseAccess
{
    public class AzureDBContext
    {
        private const string DatabaseId = "HumanPlayerStatusAppDatabase";

        private const string ContainerId = "HumanPlayerStatusData";

        private CosmosClient _Client;

        public int ResponseCode { get; set; } = -1;

        /// <summary>
        /// The Constructor for the AzureDbContext Class. It initializes the client for Azure Cosmos NoSql DB
        /// </summary>
        public AzureDBContext()
        {
            _Client = new(
                accountEndpoint: "",
                authKeyOrResourceToken: ""
                ) ;
        }


        /// <summary>
        /// Uploads/updates/inserts the Player Stats data into the Database. Its asynchronous and returns a Task.
        /// </summary>
        /// <param name="playerstats"></param>
        /// <param name="Flag"></param>
        /// <returns></returns>
        public async Task UploadPlayerStats(PlayerStats playerstats, DataIntegrityFlagStore? Flag = null) 
        {
            Database DB = _Client.GetDatabase(DatabaseId);

            Container container = DB.GetContainer(ContainerId);

            try
            {
                ItemResponse<PlayerStats> Response = await container.UpsertItemAsync(
                item: playerstats,
                partitionKey: new PartitionKey(playerstats.Id)
                );

                ResponseCode = (int)Response.StatusCode;

                // TODO: Provide Paths for all response codes.
                switch (ResponseCode)
                {
                    case 200:
                        {
                            //all went well
                            break;
                        }
                    case 201:
                        {
                            //all went well
                            break;
                        }
                    case 204: 
                        {
                            break;
                        }
                    case 400: 
                        {
                            throw new HumanPlayerStatusException((new HumanPlayerStatusExceptionMessages()).BadRequestError);
                        }
                    default:
                        {
                            //something went wrong
                            throw new HumanPlayerStatusException((new HumanPlayerStatusExceptionMessages()).UnexpectedError);
                        }
                }

            }
            catch (HumanPlayerStatusException ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        /// <summary>
        /// Reads the Player Stats data from the Database. Its asynchronous and returns a Task.
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_partitionkey"></param>
        /// <returns></returns>
        public async Task<PlayerStats> GetPlayerStats(string _id,string _partitionkey) 
        {
            Database DB = _Client.GetDatabase(DatabaseId);

            Container container = DB.GetContainer(ContainerId);

            ItemResponse<PlayerStats> response =  await container.ReadItemAsync<PlayerStats>( 
                id:_id, 
                partitionKey: new PartitionKey(_partitionkey)
                );

            return response.Resource;
        }

    }
}
