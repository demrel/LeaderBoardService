using System;
using LeaderBoardService.Data.Model;
using LeaderBoardService.Models;
using LeaderBoardService.Service.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LeaderBoardSysytem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameSession : ControllerBase
    {
        private readonly IScore _scoreService;
        private readonly IUser _userService;
        public GameSession(IScore scoreSerivce,IUser userService)
        {
            _scoreService = scoreSerivce;
            _userService = userService;
        }

        [HttpPost]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult Post([FromHeader] string sign ,[FromBody]FBUsercScoreDataModel model)
        {
            string userFBID=_userService.Validate(sign);
            
            if (userFBID!=null)
            {

                if (!_scoreService.CheckMd5S(model, sign))
                {
                    return NotFound();
                }

                User user = _userService.getByFBID(userFBID);
                if (user!=null)
                {
                    var s = _scoreService.GetSession(model.token);
                    if (s == null)
                    {
                        return NotFound(sign);
                    }

                    LeaderBoard leaderBoard = new LeaderBoard();
                    leaderBoard.Score = model.score;
                    leaderBoard.Time = DateTime.Now;
                    leaderBoard.PlayTime = model.PlayTime;
                    leaderBoard.User = user;
                    leaderBoard.Token = sign;
                    leaderBoard.session = s;
                    _scoreService.Add(leaderBoard);
                    UserResult result = new UserResult();
                    scoredbo data = _scoreService.GetUserRank2(user.ID);

                    result.Rank =(int) data.Rank;
                    result.BestScore = data.AverageScore;
                    return Ok(result);
                }
                return NotFound();
            }
            return NotFound();
        }

        [HttpGet]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult Get([FromHeader]string sign)
        {
            string userFBID = _userService.Validate(sign);
            if (userFBID != null)
            {
                if (!_scoreService.CheckMd5S(string sign))
                {
                    return NotFound();
                }

                User user = _userService.getByFBID(userFBID);
                if (user != null)
                {

                }
            return NotFound();
        }

      
    }
}
