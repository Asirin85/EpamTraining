#nullable disable
namespace Domain.Entities
{
    public record Homework
    {
        public int Id { get; init; }
        public string Task { get; init; }
    }
}
