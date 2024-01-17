using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Context
{
    public class ExamDay3DBContext : IdentityDbContext<AppUser>
    {
        public ExamDay3DBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Accessories> Accessories { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<AppUser>().HasData(
        //        new AppUser
        //        {
        //            Id= "36a25bca-b809-4023-ba23-7ddcad9e0ed7",
        //           Fullname ="Admin",
        //           UserName="Admin",
        //           Email="Admin@gmail.com",
        //           NormalizedUserName="ADMIN",
        //           NormalizedEmail="ADMIN@GMAIL.COM",
        //           EmailConfirmed=false,
        //           PasswordHash= "AQAAAAEAACcQAAAAEJl+3/ZaR9hr20wSHhOGEQQ6a1TSClJfFBaAnECUfg2z72ze2kYDXCEFvL2rxfFJ/w=="

        //        }
        //    );
        //}
    }
}
