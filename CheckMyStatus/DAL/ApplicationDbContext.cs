using CheckMyStatus.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CheckMyStatus.Domain
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<UserRequest> UserRequests { get; set; }
    }
}
