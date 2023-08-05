using CollegeHub.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeHub.Data {
    public class DBContext : DbContext{

        public DBContext(DbContextOptions<DBContext> options) : base(options) {
        }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<User>()
                .Property(p => p.Name).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<User>()
                .Property(p=> p.Email).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<User>()
                .Property(p => p.Password).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<User>()
                .Property(p => p.CPF).HasMaxLength(14).IsRequired();
            modelBuilder.Entity<User>()
                .Property(p => p.Phone).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<User>()
                .Property(p => p.Role).IsRequired();
        }

    }
}
