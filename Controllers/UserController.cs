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
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;
        public UserController(IUser userService)
        {
            _userService = userService;
        }




        [HttpGet]
        [EnableCors("_myAllowSpecificOrigins")]

        public IActionResult Get([FromHeader]string sign)
        {
           // sign = "ExPdbfp8-Ynckhz_DS7pjdfDRYckCIS-OrYumdlLPsE.eyJhbGdvcml0aG0iOiJITUFDLVNIQTI1NiIsImlzc3VlZF9hdCI6MTYwMjAxMTM5NSwicGxheWVyX2lkIjoiNDM1Mzg4NzA5MTM1MTQwNyIsInJlcXVlc3RfcGF5bG9hZCI6bnVsbH0";
            var userFBID = _userService.Validate(sign);
            if (userFBID != null)
            {
               var userData= _userService.checkUser(userFBID);
                if (userData)
                {
                    return Ok(0);
                }
                else
                {
                    return Ok(userFBID);
                }
            }
            else
            {
                return NotFound(sign);
            }
        }

       

        [HttpPost]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult Post([FromHeader] string sign,[FromBody]FBUserDataModel model)
        {
            var userFBID = _userService.Validate(sign);
            if (userFBID == null) return NotFound();
            User user = new User();
            user.Name = model.Name;
            user.PhoneNumber = model.PhoneNumber;
            user.FBID = userFBID;
            user.PhotoUrl = model.PhotoUrl;
            user.Time = DateTime.Now;
            _userService.Add(user);
            return Ok(userFBID);
        }

     
    }

}
