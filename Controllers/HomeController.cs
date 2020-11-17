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

namespace LeaderBoardService.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScore _scoreService;
        public HomeController(IScore scoreSerivce,ILogger<HomeController> logger)
        {
            _logger = logger;
            _scoreService = scoreSerivce;
        }

        public IActionResult Index()
        {
            HomeDTO model = new HomeDTO();
            model.userData= _scoreService.GetUsersRanks(100);
            return View(model);
        }

        public IActionResult Show(int id)
        {
            return View("dada");
        }


    }
}
