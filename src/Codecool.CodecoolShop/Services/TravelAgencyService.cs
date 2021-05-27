using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class TravelAgencyService
    {
        private readonly IProductDao _productDao;
        private readonly ITravelAgencyDao _travelAgencyDao;

        public TravelAgencyService(IProductDao productDao, ITravelAgencyDao travelAgencyDao)
        {
            _productDao = productDao;
            _travelAgencyDao = travelAgencyDao;
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
    }
}