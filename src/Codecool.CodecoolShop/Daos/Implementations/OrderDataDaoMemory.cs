using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Models;
using Microsoft.Extensions.Configuration;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class OrderDataDaoMemory : IOrderDataDao
    {
        private ShopContext _context;
        public OrderDataDaoMemory(IConfiguration configuration)
        {
            _context = new ShopContext(configuration);
        }

        public void Add(OrderData item)
        {
            _context.OrderDatas.Add(item);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            var itemToRemove = this.Get(id);
            if (itemToRemove != null)
            {
                _context.OrderDatas.Remove(itemToRemove);
                _context.SaveChanges();
            }
        }

        public OrderData Get(int id)
        {
            return _context.OrderDatas.FirstOrDefault(od => od.Id == id);
        }

        public IEnumerable<OrderData> GetAll()
        {
            return _context.OrderDatas.ToList();
        }
    }
}