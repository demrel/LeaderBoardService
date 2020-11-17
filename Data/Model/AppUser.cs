using System;
using Microsoft.AspNetCore.Identity;

namespace LeaderBoardService.Data.Model
{
    public class AppUser: IdentityUser
    {
        public int ID { get; set; }
        public int Name { get; set; }
    }
}
