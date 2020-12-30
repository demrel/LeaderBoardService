using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderBoardService.Data.Model
{
    public class GameSession
    {
        public int ID { get; set; }
        public string Token { get; set; }
        public DateTime startTime { get; set; }
        public string Y1L { get; set; }
        public DateTime Y1T { get; set; }
        public string Y1I { get; set; }

        public string Y2L { get; set; }
        public string Y2I { get; set; }
        public DateTime Y2T { get; set; }
        public string Y3L { get; set; }
        public string Y3I { get; set; }
        public DateTime Y3T { get; set; }
        public int UserID { get; set; }
        public User user { get; set; }
        

    }
}
