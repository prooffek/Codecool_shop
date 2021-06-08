using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class FilterServices
    {
        private TravelAgencyService _travelAgencyService;
        private ProductService _productService;
        private int _travelAgencyId;
        private int _categoryId;
        private int _countryId;

        public FilterServices(TravelAgencyService travelAgencyService, ProductService productService)
        {
            _travelAgencyService = travelAgencyService;
            _productService = productService;
        }

        private void LoadSelectedFilterOptions(ShopModel shopModel)
        {
            _travelAgencyId = shopModel.TravelAgencyId;
            _categoryId = shopModel.ProductCategoryId;
            _countryId = shopModel.CountryId;
        }

        public ShopModel Filter(ShopModel shopModel)
        {
            LoadSelectedFilterOptions(shopModel);
            var filteredProducts = SelectProducts();
            shopModel.ConfigureClassProperties(_productService, filteredProducts);
            return shopModel;
        }

        private List<Product> SelectProducts()
        {
            var filteredProducts = new List<Product>();

            if (_travelAgencyId != 0)
            {
                filteredProducts = _travelAgencyService.GetProductsForTravelAgencies(_travelAgencyId).ToList();
            }

            if (_categoryId != 0)
            {
                var productsForCategory = _productService.GetProductsForCategory(_categoryId).ToList();
                filteredProducts = productsForCategory.Where(product => filteredProducts.Contains(product)).ToList();
            }
            
            if (_countryId != 0)
            {
                var productsForCountry = _productService.GetProductsForCountry(_countryId).ToList();
                filteredProducts = productsForCountry.Where(product => filteredProducts.Contains(product)).ToList();
            }

            return filteredProducts;
        }
    }
}