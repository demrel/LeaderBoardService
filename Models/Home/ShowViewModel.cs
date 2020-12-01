using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderBoardService.Models.Home
{
    public class ShowViewModel
    {
        public List<UserScoresDTO> score { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoUrl { get; set; }
        public string Name { get; set; }
        public DateTime time { get; set; }
        public ShowViewModel()
        {
            score =new List<UserScoresDTO>();
        }
   
    }
}
