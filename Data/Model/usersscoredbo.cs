using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderBoardService.Data.Model
{
    using System;
   
        public class usersscoredbo
        {
         public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPhoto { get; set; }
        public string UserFBID { get; set; }
        public string UserTelNumber { get; set; }
        public DateTime ScoreTime { get; set; }

        public int Rank { get; set; }
       public int AverageScore { get; set; }
        }
    
}
