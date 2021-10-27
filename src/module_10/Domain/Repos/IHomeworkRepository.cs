using Domain.Entities;
using System.Collections.Generic;
#nullable enable
namespace Domain.Repos
{
    public interface IHomeworkRepository
    {
        void Delete(int id);
        int Edit(Homework homework);
        Homework? Get(int id);
        IEnumerable<Homework> GetAll();
        int New(Homework homework);
    }
}
