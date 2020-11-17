using System;
using LeaderBoardService.Data.Model;

 namespace LeaderBoardService.Service.Interfaces
{
    public interface IUser:BaseInterFace<User>
    {
        public User getByFBID(string fbID);
        public bool checkUser(string fbID);
        public string Validate(string sign);
    }
}
