using Domain.Entities;
using System.Collections.Generic;
#nullable enable
namespace Domain.Services
{
    public interface IStudentService
    {
        Student? Get(int id);
        IReadOnlyCollection<Student> GetAll();
        int New(Student student);
        int Edit(Student student);
        void Delete(int id);
    }
}
