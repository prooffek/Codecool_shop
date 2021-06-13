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
        

        public ProductDaoMemory()
        {
            
            _context = new ShopContext();
            //_data = _context.Product.ToList();
        }

        public static ProductDaoMemory GetInstance()
        {
            if (instance == null)
            {
                instance = new ProductDaoMemory();
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
            var product = _context.Product.Find(id);
            product = FillEmptyProperty(product);
            return product;
        }

        private Product FillEmptyProperty(Product product)
        {
            
            product.ProductCategory = new ProductCategoryDaoMemory().Get(product.ProductCategoryId);
            product.Country = new CountryDaoMemory().Get(product.CountryId);
            product.TravelAgency = new TravelAgencyDaoMemory().Get(product.TravelAgencyId);
            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Product.OrderBy(p => p.Name);
        }

        public IEnumerable<Product> GetBy(TravelAgency travelAgency)
        {
            IEnumerable<Product> ProductsByCategory = new List<Product>();
            var productCategoryId = travelAgency.Id;
            var temp =_context.Product.Where(x => x.ProductCategoryId == productCategoryId).ToList();
            foreach (var element in temp)
            {
                ProductsByCategory.Append(FillEmptyProperty(element));
            }

            return ProductsByCategory;
        }

        public IEnumerable<Product> GetBy(ProductCategory productCategory)
        {
            IEnumerable<Product> ProductsByCategory = new List<Product>();
            var productCategoryId = productCategory.Id;
            var temp =_context.Product.Where(x => x.ProductCategoryId == productCategoryId).ToList();
            foreach (var element in temp)
            {
                ProductsByCategory.Append(FillEmptyProperty(element));
            }
            return ProductsByCategory;
        }

        public IEnumerable<Product> GetBy(Country country)
        {
            IEnumerable<Product> ProductsByCategory = new List<Product>();
            var productCategoryId = country.Id;
            var temp =_context.Product.Where(x => x.ProductCategoryId == productCategoryId).ToList();
            foreach (var element in temp)
            {
                ProductsByCategory.Append(FillEmptyProperty(element));
            }

            return ProductsByCategory;
        }
    }
}
