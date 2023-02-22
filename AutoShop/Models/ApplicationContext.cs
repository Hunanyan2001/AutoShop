using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoShop.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
                   : base(options)
        {
            //Database.EnsureCreated();
        }
        public DbSet<PhotoUser> Photos { get; set; }   
        
        public DbSet<UserInformation> UserInformations { get; set; }
        public DbSet<CarCategory>  CarCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}