using System.ComponentModel.DataAnnotations;

namespace Exercise1.Data.Models.Authentication {
    public class LoginModel {
        [Required]
        public string Userid { get; set; }
        [Required]
        public string Password { get; set; }
    }
}