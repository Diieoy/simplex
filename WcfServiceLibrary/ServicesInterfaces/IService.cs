using System.Collections.Generic;
using System.ServiceModel;
using WcfServiceLibrary.CustomExceptions;

namespace WcfServiceLibrary.ServicesInterfaces
{
    [ServiceContract]
    public interface IService<T>
    {
        [OperationContract]
        [FaultContract(typeof(InvalidEventException))]
        void Create(T obj);

        [OperationContract]
        [FaultContract(typeof(CanNotCreateEventSeatException))]
        [FaultContract(typeof(CanNotDeleteEventException))]
        void Delete(int id);

        [OperationContract]
        [FaultContract(typeof(InvalidEventException))]
        void Update(T obj);

        [OperationContract]
        T GetById(int id);

        [OperationContract]
        IEnumerable<T> GetAll();
    }
}
