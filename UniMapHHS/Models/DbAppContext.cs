using Microsoft.EntityFrameworkCore;

namespace UniMapHHS.Models
{
    public class DbAppContext : DbContext
    {
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<BuildingFloor> BuildingFloors { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Glossary> Glossaries { get; set; }
        public DbSet<User> Users { get; set; }

        public DbAppContext(DbContextOptions<DbAppContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Favourite>()
                .HasIndex(f => f.Username);

            modelBuilder.Entity<Favourite>()
                .HasKey(f => new { f.Username, f.LocationId });

            modelBuilder.Entity<History>()
                .HasIndex(t => t.LocationId);
        }
    }
}
