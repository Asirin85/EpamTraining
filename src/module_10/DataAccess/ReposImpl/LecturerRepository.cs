using AutoMapper;
using DataAccess.Entities;
using Domain.Entities;
using Domain.Repos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.ReposImpl
{
    internal class LecturerRepository : ILecturerRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;
        public LecturerRepository(ApplicationContext applicationContext, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
        }
        public void Delete(int id)
        {
            var lecturerToDelete = _applicationContext.Lecturers.Find(id);
            _applicationContext.Entry(lecturerToDelete).State = EntityState.Deleted;
            _applicationContext.SaveChanges();
        }

        public int Edit(Lecturer lecturer)
        {
            if (_applicationContext.Lecturers.Find(lecturer.Id) is LecturerDb lecturerDb)
            {
                lecturerDb.Name = lecturer.Name;
                lecturerDb.Email = lecturer.Email;
                _applicationContext.Entry(lecturerDb).State = EntityState.Modified;
                _applicationContext.SaveChanges();
            }
            return lecturer.Id;
        }

        public Lecturer? Get(int id)
        {
            var lecturerDb = _applicationContext.Lecturers.FirstOrDefault(x => x.Id == id);
            return _mapper.Map<Lecturer?>(lecturerDb);
        }

        public IEnumerable<Lecturer> GetAll()
        {
            var lecturersDb = _applicationContext.Lecturers.ToList();
            return _mapper.Map<IReadOnlyCollection<Lecturer>>(lecturersDb);
        }

        public int New(Lecturer lecturer)
        {
            var lecturerDb = _mapper.Map<LecturerDb>(lecturer);
            var result = _applicationContext.Lecturers.Add(lecturerDb);
            _applicationContext.SaveChanges();
            return result.Entity.Id;
        }
    }
}
