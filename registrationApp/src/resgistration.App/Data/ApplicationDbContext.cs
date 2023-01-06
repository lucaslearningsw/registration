using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using resgistration.App.ViewModels;

namespace resgistration.App.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<resgistration.App.ViewModels.ProductViewModel> ProductViewModel { get; set; }
       
    }
}