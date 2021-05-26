using System.Collections.Generic;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Search
{
    public interface ISearchable
    {
        public List<ISearchable> GetSelectOptions(IEnumerable<Product> products);
    }
}