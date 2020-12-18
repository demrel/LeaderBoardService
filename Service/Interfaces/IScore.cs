using LeaderBoardService.Data.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace  LeaderBoardService.Service.Interfaces
{
    public interface IScore:BaseInterFace<LeaderBoard>
    {
        public LeaderBoard GetUserRank(int userID);
        public scoredbo GetUserRank2(int userID);
        public List<usersscoredbo> GetUsersRanks(int UserLimit);
        public  Task<List<usersscoredbo>> GetUsersRanksAsync();
        public List<LeaderBoard> GetUserScores(int userId);

        // public dynamic GetUserRankd(int userID);
        public int GetUserBestScore(int userID);

        public List<LeaderBoard> TopUSer(int limit);
        public void addTestUser();
        public void addTestScore();
        public void addTestScore(int UserID);


    }
}
