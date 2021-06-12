using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Models;
using Microsoft.Extensions.Configuration;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductDaoMemory : IProductDao
    {
        private List<Product> _data = new List<Product>();
        private static ProductDaoMemory instance = null;
        private ShopContext _context;

        public ProductDaoMemory(IConfiguration configuration)
        {
            _context = new ShopContext(configuration);
            _data = _context.Product.ToList();
        }

        public static ProductDaoMemory GetInstance(IConfiguration configuration)
        {
            if (instance == null)
            {
                instance = new ProductDaoMemory(configuration);
            }

            return instance;
        }

        public void Add(Product item)
        {
            _context.Product.Add(item);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            var itemToRemove = this.Get(id);
            if (itemToRemove != null)
            {
                _context.Product.Remove(itemToRemove);
                _context.SaveChanges();
            }
        }

        public Product Get(int id)
        {
            return _context.Product.FirstOrDefault(pc => pc.Id == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _data;
        }

        public IEnumerable<Product> GetBy(TravelAgency travelAgency)
        {
            return _data.Where(x => x.TravelAgency.Id == travelAgency.Id).ToList();
        }

        public IEnumerable<Product> GetBy(ProductCategory productCategory)
        {
            return _data.Where(x => x.ProductCategory.Id == productCategory.Id).ToList();
        }

        public IEnumerable<Product> GetBy(Country country)
        {
            return _data.Where(x => x.Country.Id == country.Id).ToList();
        }
    }
}
