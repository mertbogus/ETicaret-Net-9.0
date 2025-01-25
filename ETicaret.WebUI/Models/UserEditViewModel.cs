﻿using System.ComponentModel.DataAnnotations;

namespace ETicaret.WebUI.Models
{
    public class UserEditViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Adı")]
        public string Name { get; set; }

        [Display(Name = "SoyAdı")]
        public string Surname { get; set; }
        [Display(Name = "Mail")]
        public string Email { get; set; }
        [Display(Name = "Telefon")]
        public string? Phone { get; set; }
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        
    }
}
