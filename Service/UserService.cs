using LeaderBoardService.Data;
using LeaderBoardService.Data.Model;
using LeaderBoardService.Models;
using LeaderBoardService.Service.Interfaces;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;


namespace LeaderBoardService.Service
{
    public class UserService:IUser
    {
      private static readonly string appKey="4e220b3be3d223c9ab610c4440095021";
	//  private static readonly string appKey = "15a57323a89d93ee21b5e01fbb4f5b0e";
     //   private static readonly string appKeyTest = "1f1ea23db80418337ecd0f5c04968992"; 
        private readonly DBContext _context;
        public UserService(DBContext context)
        {
            _context = context;
        }

        public void Add(User item)
        {
            _context.User.Add(item);
            _context.SaveChanges();
        }

        public bool checkUser(string fbID)
        {
            var user = getByFBID(fbID);
            return user != null;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(User item)
        {
            throw new NotImplementedException();
        }

        public User getByFBID(string fbID)
        {
          return _context.User.Where(u => u.FBID == fbID).FirstOrDefault();
        }

        public User GetById(int id)
        {
            return _context.User.Find(id);
        }


        public string Validate(string usersing)
        {
            if (string.IsNullOrEmpty(usersing))
            {
                return null;
            }
            using (HMACSHA256 hmac = new HMACSHA256((Encoding.UTF8.GetBytes(appKey))))
            {

                try
                {
                    var firstpart = usersing.Split('.')[0];
                    var secondpart = usersing.Split('.')[1];
                    var signatureByte = Base64Decode(firstpart);
                    var signatureText = Encoding.UTF8.GetString(signatureByte);

                    var bytes = Encoding.UTF8.GetBytes(secondpart);
                    var hashValueByte = hmac.ComputeHash(bytes);
                    var hashValueText = Encoding.UTF8.GetString(hashValueByte);

                    bool validation = signatureText == hashValueText;
                    if (validation)
                    {
                        var userData = secondpart;
                        var byteText = Base64Decode(userData);
                        var jsonData = Encoding.UTF8.GetString(byteText);
                        FacebookDataModel facebookData = JsonSerializer.Deserialize<FacebookDataModel>(jsonData);
                        DateTime created = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(facebookData.issued_at);
                        DateTime now = DateTime.UtcNow;
                        double second = (now - created).TotalSeconds;
                        //check time;
                        if (second < 10)
                        {
                            return facebookData.player_id;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    //dont need more information sended streeng is incorrect;
                    return null;
                }
            }
        }

       
        private byte[] Base64Decode(string base64EncodedData)
        {
            var replaced = base64EncodedData.Replace("-", "+").Replace("_", "/");

            switch (base64EncodedData.Length % 4)
            {
                case 2: replaced += "=="; break;
                case 3: replaced += "="; break;
            }

            return System.Convert.FromBase64String(replaced);
        }

    }
}
