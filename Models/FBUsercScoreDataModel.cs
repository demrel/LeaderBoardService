using System;
namespace LeaderBoardService.Models
{
    public class FBUsercScoreDataModel
    {
        public int score { get; set; }
        public float PlayTime { get; set; }
        public string base64Img { get; set; }
        public string token { get; set; }
        public string m { get; set; }
    }
}
