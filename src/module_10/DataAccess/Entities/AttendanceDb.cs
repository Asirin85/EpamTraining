namespace DataAccess.Entities
{
    public class AttendanceDb
    {
        public int LectureId { get; set; }
        public LectureDb Lecture { get; set; }
        public int StudentId { get; set; }
        public StudentDb Student { get; set; }
        public int? Mark { get; set; }
        public bool? StudentAttended { get; set; }
    }
}
