using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class CountryService
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

        public IEnumerable<Product> GetProductsForCountry(int countryId)
        {
            Country country = _countryDao.Get(countryId);
            return _productDao.GetBy(country);
        }

        public ShopModel FilteredByCountry(ShopModel shopModel)
        {
            bool anOptionIsSelected = shopModel.CountryId != 0;

            if (anOptionIsSelected)
            {
                var productsFromTheCountry = GetProductsForCountry(shopModel.CountryId);
                shopModel.ConfigureClassProperties(_productService, productsFromTheCountry);
                return shopModel;
            }

            return new ShopModel(_productService);
        }
        
        public IEnumerable<Country> GetAllCountries()
        {
            return this._countryDao.GetAll();
        }
        
    }
}