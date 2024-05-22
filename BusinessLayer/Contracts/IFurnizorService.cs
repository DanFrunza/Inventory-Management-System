using DataAccessLayer.Models;

namespace BusinessLayer.Contracts
{
    public interface IFurnizorService
    {
        IEnumerable<Furnizor> GetAll();
        Furnizor GetById(Guid id);
        void Add(Furnizor furnizor);
        void Update(Furnizor furnizor);
        void Remove(Furnizor furnizor);
        bool Exists(Guid id);
    }
}
