using System.Collections.Generic;
#nullable disable
namespace DataAccess.Entities
{
    internal class LecturerDb
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<LectureDb> Lectures { get; set; }
    }
}
