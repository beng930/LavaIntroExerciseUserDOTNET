using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LavaIntroExerciseUser.src.UserBusinessLogic
{
    //Defines the parameters we wish to parse from the API call to the server
    public class RecentBlock
    {
        public long? height;
        public string? hash;
    }

    public class FiveBlocksObject
    {
        public List<RecentBlock>? test_results;
    }
}
