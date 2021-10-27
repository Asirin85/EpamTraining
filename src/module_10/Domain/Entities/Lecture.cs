namespace Domain.Entities
{
    public record Lecture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LecturerId { get; set; }
        public int? HomeworkId { get; set; }
        // public Lecturer Lecturer { get; set; }
        // public Homework Homework { get; set; }
    }
}
