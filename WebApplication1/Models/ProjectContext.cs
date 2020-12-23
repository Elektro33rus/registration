using Microsoft.EntityFrameworkCore;

namespace AuthProject.Models
{
    public class ProjectContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
