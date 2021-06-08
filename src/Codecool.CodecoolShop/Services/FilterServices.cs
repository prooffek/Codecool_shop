using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Search;

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

        public ShopModel Filter(ShopModel shopModel)
        {
            LoadSelectedFilterOptions(shopModel);
            var filteredProducts = SelectProducts();
            shopModel.ConfigureClassProperties(_productService, filteredProducts);
            return shopModel;
        }
        
        private void LoadSelectedFilterOptions(ShopModel shopModel)
        {
            _travelAgencyId = shopModel.TravelAgencyId;
            _categoryId = shopModel.ProductCategoryId;
            _countryId = shopModel.CountryId;
        }

        private List<Product> SelectProducts()
        {
            var filteredProducts = _productService.GetAllProducts().ToList();
            filteredProducts = UpdateFilteredProductsList<TravelAgency>(_travelAgencyService, _travelAgencyId, filteredProducts);
            filteredProducts = UpdateFilteredProductsList<ProductCategory>(_productService, _categoryId, filteredProducts);
            filteredProducts = GetProductsForCountry(filteredProducts);
            return filteredProducts;
        }

        public List<Product> GetProductsForCountry(List<Product> filteredProducts)
        {
            if (_countryId != 0)
            {
                var productsForCountry = _productService.GetProductsForCountry(_countryId).ToList();
                filteredProducts = GetCommonProducts(filteredProducts, productsForCountry);
            }
            
            return filteredProducts;
        }

        private List<Product> UpdateFilteredProductsList<T>(IService service, int id, List<Product> initialProductList) where T : IFilterable, new()
        {
            return id != 0 ? new T().GetProductForFilter(service, id, initialProductList) : initialProductList;
        }

        private List<Product> GetCommonProducts(List<Product> initialList, List<Product> newList)
        {
            return newList.Where(product => initialList.Contains(product)).ToList();
        }
    }
}