using System.Collections.Generic;
using System.ServiceModel;
using WcfServiceLibrary.DTO;

namespace WcfServiceLibrary.ServicesInterfaces
{
    [ServiceContract]
    public interface IPurchaseService
    {
        [OperationContract]
        void AddOrder(OrderDTO order);

        [OperationContract]
        List<OrderDTO> GetAllOrdersByUserId(string userId);

        [OperationContract]
        void AddPurchase(PurchaseDTO purchase);

        [OperationContract]
        void DeletePurchaseByEventSeatId(int id);

        [OperationContract]
        List<PurchaseDTO> GetAllPurchasesByUserId(string userId);

        [OperationContract]
        PurchaseDTO GetPurchaseByEventSeatId(int eventSeatId);
    }
}
