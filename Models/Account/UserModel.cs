using System;
using System.ComponentModel.DataAnnotations;


namespace LeaderBoardService.Models
{
    public class UserModel
    {

        public string ID { get; set; }

        [Required]
        [Display(Name = "Login")]
        public string UserName { get; set; }
        public bool isActive { get; set; }

        [Required]
        [Display(Name = "Ad")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }

        public string Password { get; set; }

        [Required]
        [Display(Name = "Doğum Günü")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

    }
}
