using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InternetBank.Data.User;
namespace InternetBank.Data
{
    public class InternetBankContext : IdentityDbContext<ApplicationUser> 
    {
        public InternetBankContext(
            DbContextOptions<InternetBankContext> options
        ) :base(options)
        {
            
        }

        public DbSet<ApplicationUser> Users { get; set; }

    }

}