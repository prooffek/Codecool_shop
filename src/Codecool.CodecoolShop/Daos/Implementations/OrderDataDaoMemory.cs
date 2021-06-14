using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Models;
using Microsoft.Extensions.Configuration;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class OrderDataDaoMemory : IOrderDataDao
    {
        private ShopContext _context;
        public OrderDataDaoMemory(ShopContext shopContext)
        {
            _context = shopContext;
        }

        public void Add(OrderData item)
        {
            _context.OrderData.Add(item);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            var itemToRemove = this.Get(id);
            if (itemToRemove != null)
            {
                _context.OrderData.Remove(itemToRemove);
                _context.SaveChanges();
            }
        }

        public OrderData Get(int id)
        {
            return _context.OrderData.FirstOrDefault(od => od.Id == id);
        }

        public IEnumerable<OrderData> GetAll()
        {
            return _context.OrderData.OrderBy(pc => pc.Id);
        }
    }
}