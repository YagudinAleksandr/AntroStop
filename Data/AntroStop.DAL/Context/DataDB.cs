using AntroStop.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AntroStop.DAL.Context
{
    public class DataDB : DbContext
    {
        public DbSet<Violation> Violations { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DataDB(DbContextOptions<DataDB> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Violation>()
                .HasMany<Element>()
                .WithOne(v => v.Violation)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
