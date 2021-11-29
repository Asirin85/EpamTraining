using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Repos
{
    public interface IStudentRepository
    {
        void Delete(int id);
        int Edit(Student student);
        Student? Get(int id);
        IEnumerable<Student> GetAll();
        int New(Student student);
    }
}
