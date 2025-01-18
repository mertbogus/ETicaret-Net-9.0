﻿using System.ComponentModel.DataAnnotations;

namespace ETicaret.Core.Entities
{
    public class Contact:IEntity
    {
        public int Id { get; set; }
        [Display(Name = "İsim")]
        public string Name { get; set; }
        [Display(Name = "Soyisim")]
        public string SurName { get; set; }
        public string? Email { get; set; }
        [Display(Name = "Telefon")]
        public string? Phone { get; set; }
        [Display(Name = "Mesaj")]
        public string Message { get; set; }
        [Display(Name = "Kayıt Tarihi"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; }=DateTime.Now;
    }
}
