using LeaderBoardService.Data.Model;
using LeaderBoardService.Models;
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
<<<<<<< HEAD
        public GameSession CreateSession(User user);
        public void setDataToSession(SessionDataModel model);
=======
        public GameSession CreateSession(int id);
        public void setDataToSession(string token, string log, string img);
>>>>>>> parent of 679c172... newTema
        public GameSession GetSession(string token);
        public bool CheckMd5S(FBUsercScoreDataModel input, string sign);
        public bool CheckMd5S(string sign);
    }
}
