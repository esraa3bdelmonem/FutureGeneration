using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Future_Generation.Models
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourses> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<StudentCourses>()
            //   .HasKey(x => new { x.StudentId, x.CourseId });

            modelBuilder.Entity<Course>()
                .HasKey(a => a.CourseId);

            modelBuilder.Entity<Course>()
                .Property(c => c.Cost)
                .HasColumnType("decimal(18, 2)");
            //modelBuilder.Entity<Student>()
            //    .HasIndex(e => e.Telephone)
            //    .IsUnique();
            modelBuilder.Entity<Student>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<Course>()
            .Property(c => c.Status)
            .HasConversion(
                v => v.ToString(),
                v => (CourseStatus)Enum.Parse(typeof(CourseStatus), v)
            );

        }

    }
}
