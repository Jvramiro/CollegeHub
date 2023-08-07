using CollegeHub.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeHub.Data {
    public class DBContext : DbContext{

        public DBContext(DbContextOptions<DBContext> options) : base(options) {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Exam> Exam { get; set; }
        public DbSet<Question> Question { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<User>()
                .Property(p => p.Name).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<User>()
                .Property(p=> p.Email).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<User>()
                .HasIndex(e => e.Email).IsUnique();
            modelBuilder.Entity<User>()
                .Property(p => p.Password).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<User>()
                .HasIndex(e => e.Password).IsUnique();
            modelBuilder.Entity<User>()
                .Property(p => p.CPF).HasMaxLength(14).IsRequired();
            modelBuilder.Entity<User>()
                .HasIndex(e => e.CPF).IsUnique();
            modelBuilder.Entity<User>()
                .Property(p => p.Phone).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<User>()
                .Property(p => p.Role).IsRequired();

            modelBuilder.Entity<Exam>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Exam>()
                .Property(p => p.Subject).IsRequired();
            modelBuilder.Entity<Exam>()
                .Property(p => p.TeacherId).IsRequired();
            modelBuilder.Entity<Exam>()
                .Property(p => p.Value).IsRequired();

            modelBuilder.Entity<Question>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Question>()
                .Property(p => p.ExamId).IsRequired();
            modelBuilder.Entity<Question>()
                .Property(p => p.Text).IsRequired();
            modelBuilder.Entity<Question>()
                .Property(p => p.AnswerA).IsRequired();
            modelBuilder.Entity<Question>()
                .Property(p => p.AnswerB).IsRequired();
            modelBuilder.Entity<Question>()
                .Property(p => p.CorrectAnswer).IsRequired();

        }

    }
}
