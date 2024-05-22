using DataAccessLayer.Models;

namespace BusinessLayer.Contracts
{
    public interface IProdusService
    {
        int CountRecords();
        IEnumerable<Produs> GetAll();
        Produs GetById(Guid id);
        void Add(Produs produs);
        void Update(Produs produs);
        void Remove(Produs produs);
        bool Exists(Guid id);
    }
}
