using DataModelLayer.DataModels;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        /// This method gets all the NormalQuest type items with the partition key "Quest" in the container HumanPlayerQuestData in Azure Cosmos DB.
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
                query: "SELECT * FROM HumanPlayerData c WHERE c.Id = @partitionKey AND c.QuestType = @qtype"
                )
                .WithParameter("@partitionKey", "Quest").WithParameter("@qtype", 0);


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
        /// This method gets all the EventQuest type items with the partition key "Quest" in the container HumanPlayerQuestData in Azure Cosmos DB.
        /// </summary>
        /// <returns>Task<List<EventQuestModel>></returns>
        public async Task<List<EventQuestModel>> GetItemsInEventQuestContainer()
        {
            Database db  = _Client.GetDatabase(DatabaseId);

            Container container = db.GetContainer(HumanPlayerDataContainerId);

            var parametricQuery = new QueryDefinition(
                query: "SELECT * FROM HumanPlayerData c WHERE c.Id = @partitionkey AND c.QuestType = @qtype"
                ).WithParameter("@partitionkey","Quest")
                .WithParameter("@qtype",1);

            List<EventQuestModel> EventQuests = new List<EventQuestModel>();

            using (FeedIterator<EventQuestModel> feed = container.GetItemQueryIterator<EventQuestModel>(queryDefinition:parametricQuery))
            {
                while (feed.HasMoreResults)
                {
                    FeedResponse<EventQuestModel> res = await feed.ReadNextAsync();

                    foreach (EventQuestModel e in res)
                    { 
                        EventQuests.Add(e);
                    }
                }
            }

            return EventQuests;
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

        /// <summary>
        /// Reads the Player Stats History data from the Database. Its asynchronous and returns a Task.
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_partitionkey"></param>
        /// <returns></returns>
        public async Task<StatHistory> GetStatHistory(string _id, string _partitionkey ) 
        {
            Database db = _Client.GetDatabase(DatabaseId);
            Container container = db.GetContainer(HumanPlayerDataContainerId);

            ItemResponse<StatHistory> response = await container.ReadItemAsync<StatHistory>(
                id:_id,
                partitionKey:new PartitionKey(_partitionkey)
                );

            return response.Resource;
        }

        /// <summary>
        /// Uploads/updates/inserts the Player Stats History data into the Database. Its asynchronous and returns a Task.
        /// </summary>
        /// <param name="_stathistory"></param>
        /// <returns></returns>
        public async Task UpateStatHistory(StatHistory _stathistory)
        {
            Database db = _Client.GetDatabase(DatabaseId);

            Container container = db.GetContainer(HumanPlayerDataContainerId);

            await container.UpsertItemAsync(
                item: _stathistory,
                partitionKey: new PartitionKey(_stathistory.Id)
                );
        }

        /// <summary>
        /// Creates the item statsHistory in the container.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task CreatestatHistory(StatHistory obj)
        {
            Database db = _Client.GetDatabase(DatabaseId);

            Container container = db.GetContainer (HumanPlayerDataContainerId);

            await container.CreateItemAsync<StatHistory>(
                item: obj,
                partitionKey: new PartitionKey(obj.Id)
                );
        }
    }
}
