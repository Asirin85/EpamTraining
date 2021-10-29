using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Services
{
    public interface IHomeworkService
    {
        Homework? Get(int id);
        IReadOnlyCollection<Homework> GetAll();
        int New(Homework homework);
        int Edit(Homework homework);
        void Delete(int id);
    }
}
