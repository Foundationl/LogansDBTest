using LogansDBTest.Models;
using Microsoft.EntityFrameworkCore;

namespace LogansDBTest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       public DbSet<User>? Users { get; set; }
       public DbSet<UsersAndPasswords> UsersAndPasswords { get; set; } // New table


    }
}
