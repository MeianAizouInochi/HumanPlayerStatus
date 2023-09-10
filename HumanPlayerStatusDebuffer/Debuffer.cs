using System;
using System.Threading.Tasks;
using DataModelLayer.DataModels;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace HumanPlayerStatusDebuffer
{
    public class Debuffer
    {
        private const string DatabaseId = "HumanPlayerStatusAppDatabase";

        private const string HumanPlayerDataContainerId = "HumanPlayerData";

        private CosmosClient _Client;

        [FunctionName("Debuffer")]
        public async Task Run([TimerTrigger("0 0 0 * * *")]TimerInfo myTimer, ILogger log)
        {
            //initializing client
            _Client = new(
                accountEndpoint: "",
                authKeyOrResourceToken: ""
                );

            try 
            {
                //getting current stats history from cosmos db
                StatHistory _statHis = await GetStatHistory("HumanPlayerData|Meian|StatHistory", "Meian");

                //getting current player stats
                PlayerStats ps = await GetPlayerStats("HumanPlayerData|Meian|Stats", "Meian");
               
                //Getting current datetime.
                DateTime dt = DateTime.Now;

                //creating an array of current stat history datetime objects.
                DateTime[] temp = new DateTime[]
                {
                    new DateTime(_statHis.STR_history[2],_statHis.STR_history[1],_statHis.STR_history[0]),
                    new DateTime(_statHis.AGI_history[2],_statHis.AGI_history[1],_statHis.AGI_history[0]),
                    new DateTime(_statHis.INT_history[2],_statHis.INT_history[1],_statHis.INT_history[0]),
                    new DateTime(_statHis.COMM_history[2],_statHis.COMM_history[1],_statHis.COMM_history[0]),
                    new DateTime(_statHis.MENSTB_history[2],_statHis.MENSTB_history[1],_statHis.MENSTB_history[0]),
                    new DateTime(_statHis.CRT_history[2],_statHis.CRT_history[1],_statHis.CRT_history[0]),
                    new DateTime(_statHis.HP_history[2],_statHis.HP_history[1],_statHis.HP_history[0]),
                    new DateTime(_statHis.HYG_history[2],_statHis.STR_history[1],_statHis.HYG_history[0])
                };

                //iterating and checking the results.
                //if required , then performing the update in object of playerstats.
                for (int index = 0; index < temp.Length; index++)
                {
                    if (temp[index] < dt)
                    {
                        TimeSpan ts = dt - temp[index];

                        if (ts.Days > 1)
                        {
                            ps.DebuffRates[index] += 0.1F;
                            ps.DebuffRates[index] = (float)Math.Round(ps.DebuffRates[index], 2);
                        }
                    }
                }


                //Updating the playerstats in cosmos db.
                await UploadPlayerStats(ps);
            }
            catch(CosmosException cex)
            {
                log.LogError(cex.Message);
            }
            catch(Exception ex)
            { log.LogError(ex.Message); }

            log.LogInformation($"C# Timer trigger function executed without any problems at: {DateTime.Now}");

        }

        /// <summary>
        /// Reads the Player Stats History data from the Database. Its asynchronous and returns a Task.
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_partitionkey"></param>
        /// <returns></returns>
        public async Task<StatHistory> GetStatHistory(string _id, string _partitionkey)
        {
            Database db = _Client.GetDatabase(DatabaseId);
            Container container = db.GetContainer(HumanPlayerDataContainerId);

            ItemResponse<StatHistory> response = await container.ReadItemAsync<StatHistory>(
                id: _id,
                partitionKey: new PartitionKey(_partitionkey)
                );

            return response.Resource;
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
        public async Task<PlayerStats> GetPlayerStats(string _id, string _partitionkey)
        {
            //initialize DB and Container
            Database DB = _Client.GetDatabase(DatabaseId);

            Container container = DB.GetContainer(HumanPlayerDataContainerId);

            ItemResponse<PlayerStats> response = await container.ReadItemAsync<PlayerStats>(
                id: _id,
                partitionKey: new PartitionKey(_partitionkey)
                );

            return response.Resource;
        }
    }
}
