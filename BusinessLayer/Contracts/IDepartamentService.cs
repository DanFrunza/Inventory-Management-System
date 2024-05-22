using DataAccessLayer.Models;

namespace BusinessLayer.Contracts
{
    public interface IDepartamentService
    {
        IEnumerable<Departament> GetAll();
        Departament GetById(Guid id);
        void Add(Departament departament);
        void Update(Departament departament);
        void Remove(Departament departament);
        bool Exists(Guid id);
    }
}
