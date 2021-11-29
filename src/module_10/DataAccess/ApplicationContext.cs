using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
#nullable disable
namespace DataAccess
{
    public class ApplicationContext : DbContext
    {
        internal DbSet<StudentDb> Students { get; set; }
        internal DbSet<LectureDb> Lectures { get; set; }
        internal DbSet<HomeworkDb> Homeworks { get; set; }
        internal DbSet<LecturerDb> Lecturers { get; set; }
        internal DbSet<AttendanceDb> Attendances { get; set; }
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
