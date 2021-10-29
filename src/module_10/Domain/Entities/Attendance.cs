namespace Domain.Entities
{
    public record Attendance
    {
        public int LectureId { get; init; }
        public int StudentId { get; init; }
        public int? Mark { get; init; }
        public bool? StudentAttended { get; init; }
    }
}