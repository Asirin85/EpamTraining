using AutoMapper;
using DataAccess.Entities;
using Domain.Entities;
using Domain.Repos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
#nullable enable
namespace DataAccess.ReposImpl
{
    internal class HomeworkRepository : IHomeworkRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;
        public HomeworkRepository(ApplicationContext applicationContext, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
        }
        public void Delete(int id)
        {
            var homeworkToDelete = _applicationContext.Homeworks.Find(id);
            _applicationContext.Entry(homeworkToDelete).State = EntityState.Deleted;
            _applicationContext.SaveChanges();
        }

        public int Edit(Homework homework)
        {
            if (_applicationContext.Homeworks.Find(homework.Id) is HomeworkDb homeworkDb)
            {
                homeworkDb.Task = homework.Task;
                _applicationContext.Entry(homeworkDb).State = EntityState.Modified;
                _applicationContext.SaveChanges();
            }
            return homework.Id;
        }

        public Homework? Get(int id)
        {
            var homeworkDb = _applicationContext.Homeworks.FirstOrDefault(x => x.Id == id);
            return _mapper.Map<Homework?>(homeworkDb);
        }

        public IEnumerable<Homework> GetAll()
        {
            var homeworksDb = _applicationContext.Homeworks.ToList();
            return _mapper.Map<IReadOnlyCollection<Homework>>(homeworksDb);
        }

        public int New(Homework homework)
        {
            var homeworkDb = _mapper.Map<HomeworkDb>(homework);
            var result = _applicationContext.Homeworks.Add(homeworkDb);
            _applicationContext.SaveChanges();
            return result.Entity.Id;
        }
    }
}
