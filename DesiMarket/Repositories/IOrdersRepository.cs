using DesiMarket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesiMarket.Repositories
{
    public interface IOrdersRepository
    {
        IEnumerable<Orders> GetOrders();
        IEnumerable<Orders> GetOrdersByUserId(int userId);
        Orders GetOrderById(int orderId);
        void AddOrder(Orders order);
        void UpdateOrder(Orders order);
        //void Cancelorder(int orderId);
        void DeleteOrder(Orders order);
    }
}
