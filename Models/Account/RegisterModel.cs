using System;
using System.ComponentModel.DataAnnotations;


namespace LeaderBoardService.Models
{
    public class RegisterModel
    {
   
        [Required]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Ad")]
        public string FirstName { get; set; }
      
        [Required]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }


        [Display(Name = "Doğum Günü")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Şifrə")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Şifrə uyğun gəlmir")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifrəni təsdiqləyin")]
        public string PasswordConfirm { get; set; }
    }
}
