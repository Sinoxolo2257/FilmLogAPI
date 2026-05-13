using Microsoft.EntityFrameworkCore;
using FilmLogAPI.Models;

namespace FilmLogAPI.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<User> Users { get; set; }
        public DbSet<WatchList> WatchLists { get; set; }
        public DbSet<WatchedList> WatchedLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<WatchList>().HasKey(w => w.WatchListId);
            modelBuilder.Entity<WatchedList>().HasKey(w => w.WatchedListId);

            modelBuilder.Entity<WatchList>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(w => w.UserId);
            modelBuilder.Entity<WatchedList>()
                .HasOne<User>() 
                .WithMany()
                .HasForeignKey(w => w.UserId);
        }

    }
}
