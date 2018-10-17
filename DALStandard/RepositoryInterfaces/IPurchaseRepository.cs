using DALStandard.Models;
using System.Collections.Generic;

namespace DALStandard.RepositoryInterfaces
{
    public interface IPurchaseRepository
    {
        void AddOrder(Order order);

        List<Order> GetAllOrdersByUserId(string userId);

        void AddPurchase(Purchase purchase);

        void DeletePurchaseByEventSeatId(int id);

        List<Purchase> GetAllPurchasesByUserId(string userId);

        Purchase GetPurchaseByEventSeatId(int eventSeatId);
    }
}
