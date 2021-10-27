using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class LecturerDb
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<LectureDb> Lectures { get; set; }
    }
}
