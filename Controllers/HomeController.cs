using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LeaderBoardService.Models;
using LeaderBoardService.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using LeaderBoardService.Data.Model;
using LeaderBoardService.Models.Home;
using System.Text;

namespace LeaderBoardService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScore _scoreService;
        private readonly IUser _userService;
        public HomeController(IScore scoreSerivce, IUser userService, ILogger<HomeController> logger)
        {
            _logger = logger;
            _scoreService = scoreSerivce;
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index(int id)
        {
            if (id > 200)
            {
                id = 200;
            }
            if (id == 0)
            {
                id = 100;
            }
            HomeDTO model = new HomeDTO();
            model.userData = _scoreService.GetUsersRanks(id);
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Show(int userID)
        {
            var user = _userService.GetById(userID);
            if (user != null)
            {
                ShowViewModel model = new ShowViewModel();
                List<LeaderBoard> leaderboard = _scoreService.GetUserScores(userID);
                foreach (var item in leaderboard)
                {
                    UserScoresDTO scoreDto = new UserScoresDTO() { Time = item.Time.AddHours(4), PlayTime = item.PlayTime, Score = item.Score,};
                    if (item.session!=null)
                    {
                        scoreDto.StartTime = item.session.startTime;
                        scoreDto.SessionID = item.session.ID;
                    }
                    model.score.Add(scoreDto);
                }
                model.Name = user.Name;
                model.PhoneNumber = user.PhoneNumber;
                model.PhotoUrl = user.PhotoUrl;
                model.time = user.Time.AddHours(4);
                return View(model);
            }
            else
            {
                return NotFound();
            }

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCSV()
        {

           var a= await _scoreService.GetUsersRanksAsync();
            var data = await makeString(a);

            if (data != null)
            {
                return File(data, "text/csv", "leaderboard.csv");

            }
            else {
                return BadRequest("Yeniden Cehd Edin");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ShowSession(int leaderboardID)
        {
            var gs = _scoreService.GetSessionByBoard(leaderboardID);
            if (gs != null)
            {
                ShowSessionViewModel model = new ShowSessionViewModel();
                model.Token = gs.Token;
                model.Y1I = gs.Y1I;
                model.Y1L = gs.Y1L;
                model.Y1T = gs.Y1T;

                model.Y2I = gs.Y2I;
                model.Y2L = gs.Y2L;
                model.Y2T = gs.Y2T;

                model.Y3I = gs.Y3I;
                model.Y3L = gs.Y3L;
                model.Y3T = gs.Y3T;
                return View(model);
            }
            else
            {
                return NotFound();
            }

        }
        private async Task<byte[]> makeString(List<usersscoredbo> data)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("Yeri,Adı,Xalı,Oynama Vaxtı,Telefon Nömrəsi");
                foreach (var userdata in data)
                {
                    stringBuilder.AppendLine($"{userdata.Rank},{ userdata.UserName},{ userdata.AverageScore},{userdata.ScoreTime},{userdata.UserTelNumber}");
                }
                return await Task.FromResult(Encoding.UTF8.GetBytes(stringBuilder.ToString()));
            }
            catch
            {
                return null;
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Datadelete()
        {
            return View();
        }

    }
}
