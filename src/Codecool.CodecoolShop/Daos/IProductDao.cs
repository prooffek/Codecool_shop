using Codecool.CodecoolShop.Models;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface IProductDao : IDao<Product>
    {
        IEnumerable<Product> GetBy(TravelAgency travelAgency);
        IEnumerable<Product> GetBy(ProductCategory productCategory);
        IEnumerable<Product> GetBy(Country country);
    }
}
