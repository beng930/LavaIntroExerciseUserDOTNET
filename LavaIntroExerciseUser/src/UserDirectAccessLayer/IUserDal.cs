using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LavaIntroExerciseUser.src.UserDirectAccessLayer
{
    public interface IUserDal
    {
        public Task<GetLatestBlockResponse> GetLatestBlock();
    }
}
