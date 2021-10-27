using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ApplicationContext : DbContext
    {
        public DbSet<StudentDb> Students { get; set; }
        public DbSet<LectureDb> Lectures { get; set; }
        public DbSet<HomeworkDb> Homeworks { get; set; }
        public DbSet<LecturerDb> Lecturers { get; set; }
        public DbSet<AttendanceDb> Attendances { get; set; }
        public ApplicationContext()
        {
        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AttendanceDb>().HasKey(attend => new { attend.StudentId, attend.LectureId });
        }
    }
}
