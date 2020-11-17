using LeaderBoardService.Data.Model;
using System;
using System.Collections.Generic;

namespace  LeaderBoardService.Service.Interfaces
{
    public interface IScore:BaseInterFace<LeaderBoard>
    {
        public LeaderBoard GetUserRank(int userID);
        public scoredbo GetUserRank2(int userID);
        public List<usersscoredbo> GetUsersRanks(int UserLimit);

        // public dynamic GetUserRankd(int userID);
        public int GetUserBestScore(int userID);

        public List<LeaderBoard> TopUSer(int limit);
        public void addTestUser();
        public void addTestScore();
        public void addTestScore(int UserID);


    }
}
