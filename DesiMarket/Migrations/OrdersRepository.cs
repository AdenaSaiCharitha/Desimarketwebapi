using DesiMarket.Models;
using System.Collections.Generic;
using System.Linq;

namespace DesiMarket.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly OnlineGroceryDbContext _context;
        public OrdersRepository(OnlineGroceryDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Orders> GetOrders()
        {
            return _context.Order.ToList();
        }
        public IEnumerable<Orders> GetOrdersByUserId(int userId)
        {
            return _context.Order.Where(o => o.UserId == userId).ToList();
        }
        public Orders GetOrderById(int orderId)
        {
            return _context.Order.FirstOrDefault(o => o.Id == orderId);
        }

        public void AddOrder(Orders order)
        {
            _context.Order.Add(order);
            _context.SaveChanges();
        }
        //public void Cancelorder(int orderId)
        //{

        //    _context.SaveChanges();
        //}

        public void UpdateOrder(Orders order)
        {
            _context.Order.Update(order);
            _context.SaveChanges();
        }
        public void DeleteOrder(Orders order)
        {
            _context.Order.Remove(order);
            _context.SaveChanges();
        }
    }
}
