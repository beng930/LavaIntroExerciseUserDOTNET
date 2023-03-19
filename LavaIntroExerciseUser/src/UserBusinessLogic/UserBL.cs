using LavaIntroExerciseUser.src.UserDirectAccessLayer;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LavaIntroExerciseUser.src.UserBusinessLogic
{
    public class UserBL : IUserBL
    {
        private readonly ILogger<IUserBL> _logger;
        private UserDal dal;
        // Used to keep track of the last block height writen to the file
        private long latestBlockHeight = 0;
        private string recentBlocksFilePath = "../../RecentBlocks.txt";

        public UserBL(ILogger<UserBL> logger, UserDal dal)
        {
            _logger = logger;
            this.dal = dal;
        }

        public async Task StateTrackerIteration()
        {
            var maxBlocksToWrite = 5;
            var data = new List<RecentBlock>();

            try
            {
                // Iterate over 5 consecutive blocks that were written to Cosmos and write the height and hash to the file.
                // If there was a web socket with event registration we could have used that to wait for "latest block events" instead of busy waiting, initiating requests until the next block is writen.
                for (var response = await dal.GetLatestBlock(); response.Height > latestBlockHeight && maxBlocksToWrite > 0; maxBlocksToWrite--)
                {
                    latestBlockHeight = response.Height;
                    data.Add(new RecentBlock
                    {
                        height = response.Height,
                        hash = response.Hash,
                    });
                }

                // Write 5 recent blocks to the file
                var json = JsonConvert.SerializeObject(new FiveBlocksObject { test_results = data });
                await File.AppendAllTextAsync(recentBlocksFilePath, json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                // We can potentially implement a retry policy here - if we failed continuesly for X amount of calls we terminate the program
            }
        }
    }
}
