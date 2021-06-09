using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class TravelAgencyService
    {
        private readonly IProductDao _productDao;
        private readonly ITravelAgencyDao _travelAgencyDao;
        private ProductService _productService;

        public TravelAgencyService(IProductDao productDao, ITravelAgencyDao travelAgencyDao, ProductService productService)
        {
            _productDao = productDao;
            _travelAgencyDao = travelAgencyDao;
            _productService = productService;
        }
        
        public TravelAgency GetTravelAgency(int agencyId)
        {
            return this._travelAgencyDao.Get(agencyId);
        }
        
        public IEnumerable<Product> GetProductsForTravelAgencies(int agencyId)
        {
            TravelAgency travelAgency = _travelAgencyDao.Get(agencyId);
            return _productDao.GetBy(travelAgency);
        }

        public ShopModel FilteredByTravelAgency(ShopModel shopModel)
        {
            bool anOptionIsSelected = shopModel.TravelAgencyId != 0;
            
            if (anOptionIsSelected)
            {
                var productsFromTheTravelAgency = GetProductsForTravelAgencies(shopModel.TravelAgencyId);
                shopModel.ConfigureClassProperties(_productService, productsFromTheTravelAgency);
                return shopModel;
            }

            return new ShopModel(_productService);
        }
    }
}