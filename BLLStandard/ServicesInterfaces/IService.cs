using System.Collections.Generic;

namespace BLLStandard.ServicesInterfaces
{
    public interface IService<T>
    {
        void Create(T obj);
        void Delete(int id);
        void Update(T obj);
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}
