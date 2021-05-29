using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class ProductService
    {
        private readonly IProductDao productDao;
        private readonly IProductCategoryDao productCategoryDao;
        private readonly ICountryDao countryDao;

        public ProductService(IProductDao productDao, IProductCategoryDao productCategoryDao, ICountryDao countryDao)
        {
            this.productDao = productDao;
            this.productCategoryDao = productCategoryDao;
            this.countryDao = countryDao;
        }

        public ProductCategory GetProductCategory(int categoryId)
        {
            return this.productCategoryDao.Get(categoryId);
        }

        public IEnumerable<Product> GetProductsForCategory(int categoryId)
        {
            ProductCategory category = this.productCategoryDao.Get(categoryId);
            return this.productDao.GetBy(category);
        }

        public IEnumerable<Country> GetAllCountries()
        {
            return this.countryDao.GetAll();
        }

        public IEnumerable<Product> GetProductsForCountry(int countryId)
        {
            Country country = this.countryDao.Get(countryId);
            return this.productDao.GetBy(country);
        }
    }
}
