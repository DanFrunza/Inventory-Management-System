using System.Linq.Expressions;

namespace DataAccessLayer.Repository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        void Save(); 
    }
}
