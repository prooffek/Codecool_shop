using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class ProductService : IService
    {
        private readonly IProductDao productDao;
        // private readonly IProductCategoryDao productCategoryDao;
        private readonly ICountryDao countryDao;
        private readonly IProductCategoryDao productCategoryDao;

        public ProductService(IProductDao productDao, IProductCategoryDao productCategoryDao, ICountryDao countryDao)
        {
            this.productDao = productDao;
            this.productCategoryDao = productCategoryDao;
            this.countryDao = countryDao;
        }

        // public ProductCategory GetProductCategory(int categoryId)
        // {
        //     return this.productCategoryDao.Get(categoryId);
        // }

        // public IEnumerable<Product> GetProductsForCategory(int categoryId)
        // {
        //     ProductCategory category = this.productCategoryDao.Get(categoryId);
        //     return this.productDao.GetBy(category);
        // }

        

        public Product GetProductForId(int productId)
        {
            return productDao.Get(productId);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return this.productDao.GetAll();
        }

        public IEnumerable<Product> GetProductsForId(int id)
        {
            ProductCategory category = this.productCategoryDao.Get(id);
            return this.productDao.GetBy(category);
        }
    }
}
