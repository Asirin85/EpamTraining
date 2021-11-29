#nullable disable
namespace Domain.Entities
{
    public record Lecturer
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
    }
}
