using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaderBoardService.Data;
using LeaderBoardService.Data.Model;
using LeaderBoardService.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LeaderBoardService.Service
{
    public class ScoreService : IScore
    {

        private readonly DBContext _context;

        public ScoreService(DBContext context)
        {
            _context = context;
        }
        public List<LeaderBoard> GetUserScores(int userId)
        {
            return _context.LeaderBoard.Where(l => l.UserID == userId).ToList();
           
        }
        public void Add(LeaderBoard item)
        {
            if (CheckTokenUsed(item.Token))
            {
                _context.Add(item);
                _context.SaveChanges();
            }
        }

        private bool CheckTokenUsed(string token)
        {
            var a = _context.LeaderBoard.Where(l => l.Token == token).First();
            if (a!=null)
            {
                return false;
            }
            return true;
        }

        public int GetUserBestScore(int userID)
        {
           return _context.LeaderBoard.Where(s => s.UserID == userID).OrderByDescending(s => s.Score).FirstOrDefault().Score;
        }

        public List<LeaderBoard> TopUSer(int limit)
        {
            // return _context.LeaderBoard.OrderByDescending(a => a.Score).GroupBy(u => u.ID).Take(limit).ToList();


            //  return _context.LeaderBoard.GroupBy(a => a.UserID,a=>a).ToList();
            return null;
         
        }

        public LeaderBoard GetUserRank(int userID)
        {

            string query = $@"
        WITH global_rank AS (
            SELECT ""ID"", ""UserID"", ""Score"",""PlayTime"",""Time"" , rank() OVER (ORDER BY ""Score"" DESC)   FROM ""LeaderBoard""
            )
        SELECT* FROM global_rank
        WHERE ""UserID"" = {userID} ";

             var d= _context.LeaderBoard.FromSqlRaw(query).ToList();
            LeaderBoard data = d.FirstOrDefault();
            return data;
            //return _context.LeaderBoard.Select(i => new
            //{
            //   //   RowNumber = EF.Functions.Like(i.UserID)
            //}).Where(s => s.UserID == userID);
        }

        public scoredbo GetUserRank2(int userID)
        {
            var scoreCard = _context.LeaderBoard
                    .ToList()
                    .GroupBy(u => u.UserID)
                    .OrderByDescending(grp => grp.Max(u => u.Score))
                    .Select((grp, i) => new scoredbo
                    {
                            UserId = grp.Key,
                            Rank = i + 1,
                            AverageScore = grp.Max(u => u.Score)

                    }).AsParallel()
                    .ToList().Where(u=>u.UserId==userID).FirstOrDefault();
            return scoreCard;
        }

        public void addTestUser()
        {
            Random rnd = new Random();
            List<User> users = new List<User>();
            for (int i = 0; i < 100; i++)
            {
                User user = new User();
                user.Name = $"TestUser {i}";
                user.PhoneNumber ="055"+ rnd.Next(2222222, 9999999);
                user.FBID = rnd.Next(2222222, 9999999).ToString();
                user.Time = DateTime.Now;
                users.Add(user);
            }
            _context.AddRange(users);
            _context.SaveChanges();
        }
        public void addTestScore()
        {
            Random rnd = new Random();
            var users= _context.User.ToList();
            List<LeaderBoard> boards = new List<LeaderBoard>();
            foreach (var item in users)
            {
                for (int i = 0; i < rnd.Next(2,6); i++)
                {
                    LeaderBoard board = new LeaderBoard();
                    board.Time = DateTime.Now;
                    board.UserID = item.ID;
                    board.Score = rnd.Next(10, 10000);
                    board.PlayTime = rnd.Next(100, 1000);
                    boards.Add(board);
                }
            }
            _context.AddRange(boards);
            _context.SaveChanges();
        }

        public void addTestScore(int UserID)
        {
            Random rnd = new Random();
         //   var users = _context.User.ToList();
          //  List<LeaderBoard> boards = new List<LeaderBoard>();
         
                    LeaderBoard board = new LeaderBoard();
                    board.Time = DateTime.Now;
                    board.UserID = UserID;
                    board.Score = rnd.Next(10, 10000);
                    board.PlayTime = rnd.Next(100, 1000);
                   // boards.Add(board);
          
            
            _context.Add(board);
            _context.SaveChanges();
        }

        public LeaderBoard GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(LeaderBoard item)
        {
            throw new NotImplementedException();
        }

        public List<usersscoredbo> GetUsersRanks(int UserLimit)
        {

            var scoreCard = _context.LeaderBoard.Include(l => l.User).AsParallel()
                  .GroupBy(u => u.UserID)
                  .OrderByDescending(grp => grp.Max(u => u.Score))
                  .Select((grp, i) => new usersscoredbo
                  {
                      UserId = grp.Key,
                      Rank = i + 1,
                      UserFBID=grp.Max(u => u.User.FBID),
                      UserName = grp.Max(u => u.User.Name),
                      UserPhoto = grp.Max(u => u.User.PhotoUrl),
                      UserTelNumber= grp.Max(u => u.User.PhoneNumber),
                      ScoreTime= grp.Max(u => u.Time),
                      AverageScore = grp.Max(u => u.Score)
                  }).AsParallel().Take(UserLimit).OrderBy(a => a.Rank)
                  .ToList();
            return scoreCard;
        }

        public async Task<List<usersscoredbo>> GetUsersRanksAsync()
        {
            var scoreCard =  _context.LeaderBoard.Include(l => l.User).AsParallel()
                  .GroupBy(u => u.UserID)
                  .OrderByDescending(grp => grp.Max(u => u.Score))
                  .Select((grp, i) => new usersscoredbo
                  {
                      UserId = grp.Key,
                      Rank = i + 1,
                      UserFBID = grp.Max(u => u.User.FBID),
                      UserName = grp.Max(u => u.User.Name),
                      UserPhoto = grp.Max(u => u.User.PhotoUrl),
                      UserTelNumber = grp.Max(u => u.User.PhoneNumber),
                      ScoreTime = grp.Max(u => u.Time),
                      AverageScore = grp.Max(u => u.Score)
                  }).AsParallel().OrderBy(a => a.Rank).ToList();
                  
            return await Task.FromResult(scoreCard);
        }
    }
}
