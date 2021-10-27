namespace Domain.Entities
{
    public record Attendance
    {
        public int LectureId { get; set; }
        // public Lecture Lecture { get; set; }
        public int StudentId { get; set; }
        //public Student Student { get; set; }
        public int? Mark { get; set; }
        public bool? StudentAttended { get; set; }
    }
}