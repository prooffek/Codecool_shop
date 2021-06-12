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
        private CountryService _countryService;
        private CategoryService _categoryService;
        private int _travelAgencyId;
        private int _categoryId;
        private int _countryId;

        public FilterServices(TravelAgencyService travelAgencyService, ProductService productService, 
            CountryService countryService, CategoryService categoryService)
        {
            _travelAgencyService = travelAgencyService;
            _productService = productService;
            _countryService = countryService;
            _categoryService = categoryService;
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
            filteredProducts = UpdateFilteredProductsList<ProductCategory>(_categoryService, _categoryId, filteredProducts);
            filteredProducts = UpdateFilteredProductsList<Country>(_countryService, _countryId, filteredProducts);
            return filteredProducts;
        }

        private List<Product> UpdateFilteredProductsList<T>(IService service, int id, List<Product> initialProductList) where T : BaseFilter, new()
        {
            return id != 0 ? new T().GetProductForFilter(service, id, initialProductList) : initialProductList;
        }
    }
}