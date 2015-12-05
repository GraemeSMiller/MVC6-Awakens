using Microsoft.Data.Entity;
namespace MVC6Awakens.Models
{
    
    //https://github.com/aspnet/EntityFramework/wiki/Configuring-a-DbContext
    public class DomainContext : DbContext
    {
        public DomainContext()
        {
            Database.Migrate();
        }
        public DbSet<Species> Species { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Planet> Planets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Character>()
            //    .HasKey(c => c.Id);
        }
    }
}
