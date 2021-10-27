using Domain.Entities;
using System.Collections.Generic;
#nullable enable
namespace Domain.Repos
{
    public interface ILecturerRepository
    {
        void Delete(int id);
        int Edit(Lecturer lecturer);
        Lecturer? Get(int id);
        IEnumerable<Lecturer> GetAll();
        int New(Lecturer lecturer);
    }
}
