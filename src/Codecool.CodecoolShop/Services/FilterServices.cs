using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Search;

namespace Codecool.CodecoolShop.Services
{
    public class FilterServices
    {
        private int _travelAgencyId;
        private int _categoryId;
        private int _countryId;
        
        private ProductDaoMemory _productContext;

        public FilterServices(ProductDaoMemory productContext)
        {
            _productContext = productContext;
        }

        public ShopModel Filter(ShopModel shopModel)
        {
            LoadSelectedFilterOptions(shopModel);
            var filteredProducts = SelectProducts();
            shopModel.ConfigureShopModelProperties(_productContext, filteredProducts);//_productService, filteredProducts);
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
            var filtered = _productContext.GetAll();

            filtered = _travelAgencyId == 0 ? filtered : filtered.Where(product => product.TravelAgency.Id == _travelAgencyId);
            filtered = _countryId == 0 ? filtered : filtered.Where(product => product.Country.Id == _countryId);
            filtered = _categoryId == 0 ? filtered : filtered.Where(product => product.ProductCategory.Id == _categoryId);

            return filtered.ToList();
        }
    }
}