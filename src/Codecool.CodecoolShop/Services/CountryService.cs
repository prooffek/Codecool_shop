using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class CountryService : IService
    {
        private readonly IProductDao _productDao;
        private readonly ICountryDao _countryDao;
        private ProductService _productService;

        public CountryService(IProductDao productDao, ICountryDao countryDao, ProductService productService)
        {
            _productDao = productDao;
            _countryDao = countryDao;
            _productService = productService;
        }

        public Country GetTravelAgency(int agencyId)
        {
            return this._countryDao.Get(agencyId);
        }
        
        public IEnumerable<Product> GetProductsForId(int countryId)
        {
            Country country = _countryDao.Get(countryId);
            return _productDao.GetBy(country);
        }

        public IEnumerable<Country> GetAllCountries()
        {
            return this._countryDao.GetAll();
        }
    }
}