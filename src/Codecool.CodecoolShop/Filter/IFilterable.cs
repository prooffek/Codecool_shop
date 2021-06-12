using System.Collections.Generic;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;

namespace Codecool.CodecoolShop.Search
{
    public interface IFilterable
    {
        public List<IFilterable> GetSelectOptions(IEnumerable<Product> products);
        //public List<Product> GetProductForFilter(IService service, int id, List<Product> initialList);
    }
}