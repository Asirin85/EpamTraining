#nullable disable
namespace Domain.Entities
{
    public record Lecture
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int LecturerId { get; init; }
        public int? HomeworkId { get; init; }
    }
}
