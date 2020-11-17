using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace LeaderBoardService.Models
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            userModels = new List<UserModel>();
        }
        public List<UserModel> userModels { get; set; }
    }
}
