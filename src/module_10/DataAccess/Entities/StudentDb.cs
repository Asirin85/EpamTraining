using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class StudentDb
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<AttendanceDb> AttendanceList { get; set; }
    }
}
