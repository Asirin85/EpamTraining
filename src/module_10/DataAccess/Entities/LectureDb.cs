using System.Collections.Generic;
#nullable disable
namespace DataAccess.Entities
{
    internal class LectureDb
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LecturerId { get; set; }
        public LecturerDb Lecturer { get; set; }
        public int? HomeworkId { get; set; }
        public HomeworkDb Homework { get; set; }
        public ICollection<AttendanceDb> AttendanceList { get; set; }
    }
}
