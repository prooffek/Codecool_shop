using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductCategoryDaoMemory : IProductCategoryDao
    {
        private ShopContext _context;
        private List<ProductCategory> _data;
        private static ProductCategoryDaoMemory instance = null;

        public ProductCategoryDaoMemory()
        {
            _context = new ShopContext();
            //_data = _context.ProductCategory.ToList();
        }

        public static ProductCategoryDaoMemory GetInstance()
        {
            if (instance == null)
            {
                instance = new ProductCategoryDaoMemory();
            }
            return instance;
        }

        public void Add(ProductCategory item)
        {
            _context.ProductCategory.Add(item);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            var itemToRemove = this.Get(id);
            if (itemToRemove != null)
            {
                _context.ProductCategory.Remove(itemToRemove);
                _context.SaveChanges();
            }
            
        }

        public ProductCategory Get(int id)
        {
            return _context.ProductCategory.FirstOrDefault(pc => pc.Id == id);
        }
        

        public IEnumerable<ProductCategory> GetAll()
        {
            return _context.ProductCategory.OrderBy(pc => pc.Name);
        }
    }
}
