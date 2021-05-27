using System.Collections.Generic;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Search
{
    public interface IFilterable
    {
        public List<IFilterable> GetSelectOptions(IEnumerable<Product> products);
    }
}