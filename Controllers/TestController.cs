using LeaderBoardService.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeaderBoardSysytem.Controllers
{ 
    public class TestController : Controller
    {
        private readonly IScore _scoreService;
         public TestController(IScore scoreService)
          {
            _scoreService = scoreService;
          }

        public IActionResult setUser()
        {
            _scoreService.addTestUser();
            return Ok();
        }

        public IActionResult Tops(int toplimit)
        {
            //_scoreService.TopUSer(50);
            return Ok(_scoreService.TopUSer(50));
        }

   
        public IActionResult setScore(int id)
        {
            _scoreService.addTestScore(id);
            var res=_scoreService.GetUserRank(id);
            return Ok(res);
        }
        public IActionResult getRank(int id)
        {
           var a= _scoreService.GetUserRank2(id);
            return Ok(a);
        }
    }
}
