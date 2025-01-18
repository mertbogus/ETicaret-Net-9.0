using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Core.Entities
{
    public class AppUser : IEntity
    {
        public int Id { get; set; }
        [Display(Name="Adı")]
        public string Name { get; set; }

        [Display(Name = "SoyAdı")]
        public string Surname { get; set; }
        [Display(Name = "Mail")]
        public string Email { get; set; }
        [Display(Name = "Telefon")]
        public string? Phone { get; set; }
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [Display(Name = "Kullanıcı Adı")]
        public string? UserName { get; set; } = string.Empty;
        [Display(Name = "Aktif?")]
        public bool IsActive { get; set; }
        [Display(Name = "Admin"), ScaffoldColumn(false)]
        public bool IsAdmin { get; set; }
        [Display(Name = "Kayıt Tarihi"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public Guid? UserGuid { get; set; } = Guid.NewGuid();
    }
}
