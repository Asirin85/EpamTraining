#nullable disable
namespace Domain.Entities
{
    public record Student
    {
        public int Id { get; init; }
        public string Name { get; init; }

        public string Email { get; init; }
        public string PhoneNumber { get; init; }

    }
}
