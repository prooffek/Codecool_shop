using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    class ProductCategoryDaoMemory : IProductCategoryDao
    {
        private ShopContext _context;
        private List<ProductCategory> _data;

        private IConfiguration _Configuration { get; set; }
        //private List<ProductCategory> data = new List<ProductCategory>();
        private static ProductCategoryDaoMemory instance = null;

        

        public ProductCategoryDaoMemory(IConfiguration configuration)
        {
            _context = new ShopContext(configuration);
            _data = _context.ProductCategories.ToList();
            
        }

        public static ProductCategoryDaoMemory GetInstance(IConfiguration configuration)
        {
            if (instance == null)
            {
                instance = new ProductCategoryDaoMemory(configuration);
            }
            return instance;
        }

        public void Add(ProductCategory item)
        {
            _context.ProductCategories.Add(item);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            var itemToRemove = this.Get(id);
            if (itemToRemove != null)
            {
                _context.ProductCategories.Remove(itemToRemove);
                _context.SaveChanges();
            }
            
        }

        public ProductCategory Get(int id)
        {
            return _context.ProductCategories.FirstOrDefault(pc => pc.Id == id);
        }
        

        public IEnumerable<ProductCategory> GetAll()
        {
            return _data;
        }
    }
}
