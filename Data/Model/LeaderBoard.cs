using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderBoardService.Data.Model
{
    using System;
   
        public class LeaderBoard
        {
        public int ID { get; set; }
            public int Score { get; set; }
            public DateTime Time { get; set; }
            public float PlayTime { get; set; }
            public int UserID { get; set; }
            public int? rank { get; set; }
            public User User { get; set; }
        }
    
}
