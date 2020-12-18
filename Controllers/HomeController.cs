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
                    UserScoresDTO scoreDto = new UserScoresDTO() { Time = item.Time.AddHours(4), PlayTime = item.PlayTime, Score = item.Score };
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
