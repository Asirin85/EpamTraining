using Domain.Entities;
using Domain.Repos;
using Domain.Services;
using System.Collections.Generic;
using System.Linq;
#nullable enable
namespace BusinessLogic.Services
{
    internal class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void Delete(int id)
        {
            _studentRepository.Delete(id);
        }

        public int Edit(Student student)
        {
            return _studentRepository.Edit(student);
        }

        public Student? Get(int id)
        {
            return _studentRepository.Get(id);
        }

        public IReadOnlyCollection<Student> GetAll()
        {
            return _studentRepository.GetAll().ToArray();
        }

        public int New(Student student)
        {
            return _studentRepository.New(student);
        }
    }
}
