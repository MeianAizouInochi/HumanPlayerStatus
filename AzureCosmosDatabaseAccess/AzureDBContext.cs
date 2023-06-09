using DataModelLayer.DataModels;
using DataModelLayer.DataIntegrityStore;
using HumanPlayerStatusExceptions;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using System.Windows;
using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Security.RightsManagement;

namespace AzureCosmosDatabaseAccess
{
    public class AzureDBContext
    {
        private const string DatabaseId = "HumanPlayerStatusAppDatabase";

        private const string HumanPlayerDataContainerId = "HumanPlayerData";

        private const string Quest_PartitionKey = "Quest";

        private CosmosClient _Client;

        /// <summary>
        /// The Constructor for the AzureDbContext Class. It initializes the client for Azure Cosmos NoSql DB
        /// </summary>
        public AzureDBContext()
        {
            //initializing client
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
        public async Task UploadPlayerStats(PlayerStats playerstats) 
        {
            //initialize Db and container reference.
            Database DB = _Client.GetDatabase(DatabaseId);

            Container container = DB.GetContainer(HumanPlayerDataContainerId);

            //Request-POST type
            ItemResponse<PlayerStats> Response = await container.UpsertItemAsync(
                item: playerstats,
                partitionKey: new PartitionKey(playerstats.Id)
                );

        }


        /// <summary>
        /// Reads the Player Stats data from the Database. Its asynchronous and returns a Task.
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_partitionkey"></param>
        /// <returns></returns>
        public async Task<PlayerStats> GetPlayerStats(string _id,string _partitionkey) 
        {
            //initialize DB and Container
            Database DB = _Client.GetDatabase(DatabaseId);

            Container container = DB.GetContainer(HumanPlayerDataContainerId);

            ItemResponse<PlayerStats> response =  await container.ReadItemAsync<PlayerStats>( 
                id:_id, 
                partitionKey: new PartitionKey(_partitionkey)
                );

            return response.Resource;
        }


        /// <summary>
        /// This method gets all the items with the partition key "Quest" in the container HumanPlayerQuestData in Azure Cosmos DB.
        /// </summary>
        /// <returns>Task<List<QuestModel>></returns>
        public async Task<List<QuestModel>> GetItemsInQuestContainer() 
        {
            //initialize DB and Container
            Database Db = _Client.GetDatabase(DatabaseId);

            Container container =  Db.GetContainer(HumanPlayerDataContainerId);

            //Creating Query using the QueryDefinition Class.
            var parameterizedQuery = new QueryDefinition
                (
                query: "SELECT * FROM HumanPlayerData c WHERE c.Id = @partitionKey"
                )
                .WithParameter("@partitionKey", "Quest");


            //Creating a Feed Iterator to read through page, and passing QueryDefinition
            using FeedIterator<QuestModel> feed  = container.GetItemQueryIterator<QuestModel>(
                queryDefinition: parameterizedQuery
                );

            //This will be returned.
            List<QuestModel> Quests = new List<QuestModel>();

            while (feed.HasMoreResults)
            {
                FeedResponse<QuestModel> res = await feed.ReadNextAsync();

                //Adding Each item from the Reponse containing number of items, which is the amount of items a page can contain in Azure settings.
                foreach (QuestModel _quest in res)
                {
                    Quests.Add(_quest);
                }
            }

            //Returning the populated List.
            return Quests;
        }


        /// <summary>
        /// This Function Updates the Quest Item in the Container.
        /// </summary>
        /// <param name="_Quest"></param>
        /// <returns></returns>
        public async Task UpdateQuest(QuestModel _Quest) 
        {
            Database DB = _Client.GetDatabase(DatabaseId);

            Container container = DB.GetContainer(HumanPlayerDataContainerId);

            ItemResponse<QuestModel> response = await container.UpsertItemAsync(
                     item: _Quest,
                     partitionKey: new PartitionKey(Quest_PartitionKey)
                     );

        }

        /// <summary>
        /// This function creates the Quest Item in the CosmosDb.
        /// </summary>
        /// <param name="_Quest"></param>
        /// <returns></returns>
        public async Task CreateQuest(QuestModel _Quest)
        {
            Database DB = _Client.GetDatabase(DatabaseId);

            Container container = DB.GetContainer(HumanPlayerDataContainerId);

            ItemResponse<QuestModel> response =  await container.CreateItemAsync<QuestModel>
                (
                item:_Quest,
                partitionKey: new PartitionKey(Quest_PartitionKey)
                );

        }
    }
}
