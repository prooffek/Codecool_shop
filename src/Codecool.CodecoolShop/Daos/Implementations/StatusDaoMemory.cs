using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Models;
using Microsoft.EntityFrameworkCore;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class StatusDaoMemory : IStatusDao
    {
        private ShopContext _shopContext;

        public StatusDaoMemory(ShopContext db)
        {
            _shopContext = db;
        }

        public StatusDaoMemory()
        {
            _shopContext = new ShopContext();
        }
        
        public void Add(Status item)
        {
            _shopContext.Add(item);
            _shopContext.SaveChanges();
        }

        public void Remove(int id)
        {
            Status statusToRemove = Get(id);
            _shopContext.Remove(statusToRemove);
            _shopContext.SaveChanges();
        }

        public Status Get(int id)
        {
            return _shopContext.OrderStatus.Find(id);
        }

        public IEnumerable<Status> GetAll()
        {
            return _shopContext.OrderStatus.OrderBy(status => status.Name);
        }
    }
}