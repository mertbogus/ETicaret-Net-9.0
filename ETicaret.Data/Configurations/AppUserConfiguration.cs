using ETicaret.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETicaret.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x=>x.Name).IsRequired().HasColumnType("varchar(50)").HasMaxLength(50);
            builder.Property(x => x.Surname).IsRequired().HasColumnType("varchar(50)").HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasColumnType("varchar(50)").HasMaxLength(50);
            builder.Property(x => x.Phone).HasColumnType("varchar(15)").HasMaxLength(15);
            builder.Property(x => x.Password).IsRequired().HasColumnType("nvarchar(50)").HasMaxLength(50);
            builder.Property(x => x.UserName).HasColumnType("varchar(50)").HasMaxLength(50);

            //ilk kullanıcı açıldığında admin kaydı oluştursun test amaçlı
            builder.HasData(
                new AppUser
                {
                    Id = 1,
                    CreateDate = DateTime.Now,
                    UserName = "Admin",
                    Email = "admin@eticaret.com",
                    IsActive = true,
                    IsAdmin =true,
                    Name="Mert",
                    Surname="Böğüş",
                    Password="123456*"
                }
                );

        }
    }
}
