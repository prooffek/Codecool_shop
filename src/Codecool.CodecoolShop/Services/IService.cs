using System.Collections.Generic;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public interface IService
    {
        public IEnumerable<Product> GetProductsForId(int id);
    }
}