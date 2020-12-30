using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaderBoardService.Data;
using LeaderBoardService.Data.Model;
using LeaderBoardService.Models;
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
            return _context.LeaderBoard.Include(l=>l.session).Where(l => l.UserID == userId).ToList();
           
        }
        public void Add(LeaderBoard item)
        {
            if (CheckTokenUsed(item.Token,item.User.ID))
            {
                _context.Add(item);
                _context.SaveChanges();
            }
        }

        private bool CheckTokenUsed(string token,int id)
        {
            var a = _context.LeaderBoard.Where(l=>l.UserID==id)
                .Where(l => l.Token == token).FirstOrDefault();
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

        public GameSession CreateSession(User user)
        {
            GameSession gs = new GameSession();
            gs.Token = Guid.NewGuid().ToString();
            gs.startTime = DateTime.UtcNow;
            gs.user = user;
            _context.Add(gs);
            _context.SaveChanges();
            return gs;
        }
        public void setDataToSession(SessionDataModel model)
        {
            var session=GetSession(model.dad);
            if (session==null)
            {
                return;
            }
            if (string.IsNullOrEmpty(session.Y1L))
            {
                session.Y1L = model.Log;
                session.Y1T = DateTime.UtcNow;
                session.Y1I = model.ada;
            }
            else if (string.IsNullOrEmpty(session.Y2L))
            {
                session.Y2L = model.Log;
                session.Y2T = DateTime.UtcNow;
                session.Y2I = model.ada;
            }
            else if (string.IsNullOrEmpty(session.Y3L))
            {
                session.Y3L = model.Log;
                session.Y3T = DateTime.UtcNow;
                session.Y3I = model.ada;
            }
            _context.GameSession.Update(session);
            _context.SaveChanges();
        }
        public GameSession GetSession(string token)
        {
            return _context.GameSession.Where(g => g.Token == token).FirstOrDefault();
        }
        public GameSession GetSessionByBoard(int  id)
        {
            return _context.GameSession.Find(id);
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

        public  bool CheckMd5S(FBUsercScoreDataModel input,string sign)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                string m = "";
                string s = "";
                try
                {
                    var ar = sign.Split('.');
                    m = ar[2];
                    s = ar[0]+"." + ar[1];
                }
                catch (Exception)
                {

                    return false;
                }

                var a = s + s.Length * 151555 + s.Substring(0, 10) + "csrun";
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(a);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString().ToLower()==m;
            }
        }
        public bool CheckMd5S(SessionDataModel input, string sign)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                string m = "";
                string s = "";
                try
                {
                    var ar = sign.Split('.');
                    m = ar[2];
                    s = ar[0]+"." + ar[1];
                }
                catch (Exception)
                {

                    return false;
                }

                var a = s + s.Length * 151555 + s.Substring(0, 10) + "csrun";
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(a);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString().ToLower() == m;
            }
        }
        public bool CheckMd5S(string sign)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                string m = "";
                string s = "";
                try
                {
                    var ar = sign.Split('.');
                    m = ar[2];
                    s = ar[0]+"." + ar[1];
                }
                catch (Exception)
                {

                    return false;
                }

                var a = s + s.Length * 151555 + s.Substring(0, 10)+"csrun"; 
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(a);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString().ToLower() == m;
            }
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
