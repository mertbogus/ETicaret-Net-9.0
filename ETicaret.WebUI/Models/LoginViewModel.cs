using System.ComponentModel.DataAnnotations;

namespace ETicaret.WebUI.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Mail"), DataType(DataType.EmailAddress), Required(ErrorMessage ="Email Boş Geçilemez.")]
        public string Email { get; set; }

        [Display(Name = "Şifre"), DataType(DataType.Password), Required(ErrorMessage = "Şifre Boş Geçilemez.")]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }

        public bool RememberMe { get; set; }
    }
}
