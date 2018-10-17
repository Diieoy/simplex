using System.Collections.Generic;
using System.Security.Permissions;
using WcfServiceLibrary.DTO;
using WcfServiceLibrary.ServicesInterfaces;

namespace WcfServiceLibrary.Services
{
    public class PurchaseService : IPurchaseService
    {
        private BLLStandard.ServicesInterfaces.IPurchaseService purchaseService;

        public PurchaseService(BLLStandard.ServicesInterfaces.IPurchaseService purchaseService)
        {
            this.purchaseService = purchaseService;
        }
        [PrincipalPermission(SecurityAction.Demand, Role = "user")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void AddOrder(OrderDTO order)
        {
            purchaseService.AddOrder(FromWcfOrderDTOToBllOrderDTO(order));
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "user")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void AddPurchase(PurchaseDTO purchase)
        {
            purchaseService.AddPurchase(FromWcfPurchaseDTOToBllPurchaseDTO(purchase));
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "user")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void DeletePurchaseByEventSeatId(int id)
        {
            purchaseService.DeletePurchaseByEventSeatId(id);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "user")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public List<OrderDTO> GetAllOrdersByUserId(string userId)
        {
            var orders = purchaseService.GetAllOrdersByUserId(userId);

            List<OrderDTO> list = new List<OrderDTO>();
            foreach (var item in orders)
            {
                list.Add(FromBllOrderDTOToWcfOrderDTO(item));
            }

            return list;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "user")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public List<PurchaseDTO> GetAllPurchasesByUserId(string userId)
        {
            var purchases = purchaseService.GetAllPurchasesByUserId(userId);

            List<PurchaseDTO> list = new List<PurchaseDTO>();
            foreach (var item in purchases)
            {
                list.Add(FromBllPurchaseDTOToWcfPurchaseDTO(item));
            }

            return list;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "user")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public PurchaseDTO GetPurchaseByEventSeatId(int eventSeatId)
        {
            var result = purchaseService.GetPurchaseByEventSeatId(eventSeatId);

            if(result == null)
            {
                return null;
            }

            return FromBllPurchaseDTOToWcfPurchaseDTO(result);
        }

        private BLLStandard.DTO.OrderDTO FromWcfOrderDTOToBllOrderDTO(OrderDTO wcfOrderDTO)
        {
            return new BLLStandard.DTO.OrderDTO()
            {
                Id = wcfOrderDTO.Id,
                UserId = wcfOrderDTO.UserId,
                SeatId = wcfOrderDTO.SeatId,
                DateTimeOrder = wcfOrderDTO.DateTimeOrder
            };
        }

        private OrderDTO FromBllOrderDTOToWcfOrderDTO(BLLStandard.DTO.OrderDTO orderDTO)
        {
            return new OrderDTO()
            {
                Id = orderDTO.Id,
                UserId = orderDTO.UserId,
                SeatId = orderDTO.SeatId,
                DateTimeOrder = orderDTO.DateTimeOrder
            };
        }

        private BLLStandard.DTO.PurchaseDTO FromWcfPurchaseDTOToBllPurchaseDTO(PurchaseDTO wcfPurchaseDTO)
        {
            return new BLLStandard.DTO.PurchaseDTO()
            {
                Id = wcfPurchaseDTO.Id,
                UserId = wcfPurchaseDTO.UserId,
                EventSeatId = wcfPurchaseDTO.EventSeatId
            };
        }

        private PurchaseDTO FromBllPurchaseDTOToWcfPurchaseDTO(BLLStandard.DTO.PurchaseDTO purchaseDTO)
        {
            return new PurchaseDTO()
            {
                Id = purchaseDTO.Id,
                UserId = purchaseDTO.UserId,
                EventSeatId = purchaseDTO.EventSeatId
            };
        }
    }
}
