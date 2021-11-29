using AutoMapper;
using DataAccess.Entities;
using Domain.Entities;
using Domain.Repos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.ReposImpl
{
    internal class StudentRepository : IStudentRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;
        public StudentRepository(ApplicationContext applicationContext, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
        }
        public void Delete(int id)
        {
            var studentToDelete = _applicationContext.Students.Find(id);
            _applicationContext.Entry(studentToDelete).State = EntityState.Deleted;
            _applicationContext.SaveChanges();
        }

        public int Edit(Student student)
        {
            if (_applicationContext.Students.Find(student.Id) is StudentDb studentDb)
            {
                studentDb.Name = student.Name;
                studentDb.Email = student.Email;
                studentDb.PhoneNumber = student.PhoneNumber;
                _applicationContext.Entry(studentDb).State = EntityState.Modified;
                _applicationContext.SaveChanges();
            }
            return student.Id;
        }

        public Student? Get(int id)
        {
            var studentDb = _applicationContext.Students.FirstOrDefault(x => x.Id == id);
            return _mapper.Map<Student?>(studentDb);
        }

        public IEnumerable<Student> GetAll()
        {
            var studentsDb = _applicationContext.Students.ToList();
            return _mapper.Map<IReadOnlyCollection<Student>>(studentsDb);
        }

        public int New(Student student)
        {
            var studentDb = _mapper.Map<StudentDb>(student);
            var result = _applicationContext.Students.Add(studentDb);
            _applicationContext.SaveChanges();
            return result.Entity.Id;
        }
    }
}
