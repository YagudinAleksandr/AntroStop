using AntroStop.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AntroStop.DAL.Context
{
    public class DataDB : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        
        //public DbSet<Organization> Organizations { get; set; }
        //public DbSet<OrganizationUser> OrganizationUsers { get; set; }

        public DbSet<Violation> Violations { get; set; }
        public DbSet<Element> Elements { get; set; }
        
        public DataDB(DbContextOptions<DataDB> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            /*
            modelBuilder.Entity<Organization>()
                .HasIndex(p => new { p.Msrn, p.Itn })
                .IsUnique(true);*/

            modelBuilder.Entity<Role>()
                .HasMany<User>()
                .WithOne(r => r.Role)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>()
                .HasIndex(i => i.ID)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany<Violation>()
                .WithOne(r => r.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Violation>()
                .HasMany<Element>()
                .WithOne(v => v.Violation)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
