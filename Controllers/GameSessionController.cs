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
        public IActionResult Post([FromHeader] string sign ,[FromBody] SessionDataModel model)
        {
            string userFBID = _userService.Validate(sign);
            // var userFBID = "3485479291543174";
            if (userFBID != null)
            {
                if (!_scoreService.CheckMd5S(model, sign))
                {
                    return NotFound();
                }

                User user = _userService.getByFBID(userFBID);
                if (user != null)
                {
                    var s = _scoreService.GetSession(model.dad);
                    if (s == null)
                    {
                        return NotFound();
                    }

                    _scoreService.setDataToSession(model);
                    return Ok();
                }
                return NotFound();
            }
            return NotFound();
        }

        [HttpGet]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult Get([FromHeader] string sign)
        {
            string userFBID = _userService.Validate(sign);
            if (userFBID != null)
            {
                if (!_scoreService.CheckMd5S(sign))
                {
                    return NotFound();
                }

                User user = _userService.getByFBID(userFBID);
                if (user != null)
                {
                  var gameSes=  _scoreService.CreateSession(user.ID);
                    if (gameSes!=null)
                    {
                        return Ok(gameSes.Token);
                    }
                  
                }
            }
            return NotFound();
        }

      
    }
}
