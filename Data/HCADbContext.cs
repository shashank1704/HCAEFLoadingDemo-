using HCAEFLoadingDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace HCAEFLoadingDemo.Data
{
    public class HCADbContext : DbContext
    {
        public HCADbContext(DbContextOptions<HCADbContext> options) : base(options) { }

        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Healthcare");

            modelBuilder.Entity<Shift>()
                .HasOne(s => s.Nurse)
                .WithMany(n => n.Shifts)
                .HasForeignKey(s => s.NurseID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

