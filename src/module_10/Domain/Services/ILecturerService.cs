using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Services
{
    public interface ILecturerService
    {
        Lecturer? Get(int id);
        IReadOnlyCollection<Lecturer> GetAll();
        int New(Lecturer lecturer);
        int Edit(Lecturer lecturer);
        void Delete(int id);
    }
}
