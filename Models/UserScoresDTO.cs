using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderBoardService.Models
{
    public class UserScoresDTO
    {
        public int Score { get; set; }
        public DateTime Time { get; set; }
        public float PlayTime { get; set; }
        public DateTime StartTime { get; set; }
        public int? SessionID { get; set; }

    }
}
