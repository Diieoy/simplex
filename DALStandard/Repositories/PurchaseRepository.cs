using DALStandard.Models;
using DALStandard.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace DALStandard.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        public void AddOrder(Order order)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.Order.Add(order);
                db.SaveChanges();
            }
        }

        public List<Order> GetAllOrdersByUserId(string userId)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var res = from o in db.Order
                          where o.UserId == userId
                          select o;

                return res.ToList();
            }
        }

        public void AddPurchase(Purchase purchase)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.Purchase.Add(purchase);
                db.SaveChanges();
            }
        }

        public void DeletePurchaseByEventSeatId(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.Purchase.Remove(db.Purchase.FirstOrDefault(x => x.EventSeatId == id));
                db.SaveChanges();
            }
        }

        public List<Purchase> GetAllPurchasesByUserId(string userId)
        {
            using (MyDbContext db = new MyDbContext())
            {
                return (from p in db.Purchase
                        where p.UserId == userId
                        select p).ToList();
            }
        }

        public Purchase GetPurchaseByEventSeatId(int eventSeatId)
        {
            using (MyDbContext db = new MyDbContext())
            {
                return db.Purchase.FirstOrDefault(e => e.EventSeatId == eventSeatId);
            }
        }
    }
}
