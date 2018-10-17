using BLLStandard.DTO;
using BLLStandard.ServicesInterfaces;
using DALStandard.Models;
using DALStandard.RepositoryInterfaces;
using System.Collections.Generic;

namespace BLLStandard.Services
{
    public class PurchaseService : IPurchaseService
    {
        private IPurchaseRepository repository;

        public PurchaseService(IPurchaseRepository repository)
        {
            this.repository = repository;
        }

        public void AddOrder(OrderDTO o)
        {
            repository.AddOrder(new Order
            {
                Id = o.Id,
                UserId = o.UserId,
                SeatId = o.SeatId,
                DateTimeOrder = o.DateTimeOrder
            });
        }

        public void AddPurchase(PurchaseDTO p)
        {
            repository.AddPurchase(new Purchase
            {
                UserId = p.UserId,
                EventSeatId = p.EventSeatId
            });
        }

        public void DeletePurchaseByEventSeatId(int id)
        {
            repository.DeletePurchaseByEventSeatId(id);
        }

        public List<OrderDTO> GetAllOrdersByUserId(string userId)
        {
            var orders = repository.GetAllOrdersByUserId(userId);

            List<OrderDTO> list = new List<OrderDTO>();

            foreach (var item in orders)
            {
                list.Add(new OrderDTO
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    SeatId = item.SeatId,
                    DateTimeOrder = item.DateTimeOrder
                });
            }

            return list;
        }

        public List<PurchaseDTO> GetAllPurchasesByUserId(string userId)
        {
            var purchases = repository.GetAllPurchasesByUserId(userId);

            List<PurchaseDTO> list = new List<PurchaseDTO>();

            foreach (var item in purchases)
            {
                list.Add(new PurchaseDTO
                {
                    UserId = item.UserId,
                    EventSeatId = item.EventSeatId
                });
            }

            return list;
        }

        public PurchaseDTO GetPurchaseByEventSeatId(int eventSeatId)
        {
            var purchase = repository.GetPurchaseByEventSeatId(eventSeatId);

            if (purchase == null)
            {
                return null;
            }

            return new PurchaseDTO
            {
                Id = purchase.Id,
                EventSeatId = purchase.EventSeatId,
                UserId = purchase.UserId
            };
        }
    }
}
