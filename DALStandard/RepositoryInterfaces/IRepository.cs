using System.Collections.Generic;

namespace DALStandard.RepositoryInterfaces
{
    public interface IRepository<T>
    {
        void Create(T obj);
        void Delete(int id);
        void Update(T obj);
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}
