using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LavaIntroExerciseUser.src.UserDirectAccessLayer
{
    public class UserDal : IUserDal
    {
        private ILogger<IUserDal> _logger;
        private ProxyServerService.ProxyServerServiceClient client;

        public UserDal(ILogger<UserDal> logger, ProxyServerService.ProxyServerServiceClient client)
        {
            _logger = logger;
            this.client = client;
        }

        public async Task<GetLatestBlockResponse> GetLatestBlock()
        {
            return await client.GetLatestBlockAsync(
                  new GetLatestBlockRequest { });
        }
    }
}
