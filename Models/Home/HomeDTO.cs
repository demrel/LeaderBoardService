using LeaderBoardService.Data.Model;
using System;
using System.Collections.Generic;

namespace LeaderBoardService.Models
{
    public class HomeDTO
    {
        public HomeDTO()
        {
            userData = new List<usersscoredbo>();
        }
       public List<usersscoredbo> userData { get; set; }
    }
}
