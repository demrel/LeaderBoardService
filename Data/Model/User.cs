using System;
using System.Collections.Generic;

namespace LeaderBoardService.Data.Model
{
    public class User
    {
        public User()
        {
            scores = new List<LeaderBoard>();
        }
        public int ID { get; set; }
        public string FBID { get; set; }
        public string PhoneNumber { get; set; }
        public List<LeaderBoard> scores { get; set; }
        public string PhotoUrl { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        
    }
}
