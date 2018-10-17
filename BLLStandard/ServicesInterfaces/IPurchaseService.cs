using BLLStandard.DTO;
using System.Collections.Generic;

namespace BLLStandard.ServicesInterfaces
{
    public interface IPurchaseService
    {
        void AddOrder(OrderDTO order);

        List<OrderDTO> GetAllOrdersByUserId(string userId);

        void AddPurchase(PurchaseDTO purchase);

        void DeletePurchaseByEventSeatId(int id);

        List<PurchaseDTO> GetAllPurchasesByUserId(string userId);

        PurchaseDTO GetPurchaseByEventSeatId(int eventSeatId);
    }
}
