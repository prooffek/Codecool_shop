using System.Collections.Generic;
using Codecool.CodecoolShop.Search;

namespace Codecool.CodecoolShop.Models
{
    public class Country: BaseModel, IFilterable
    {
        public List<Product> Products { get; set; }
        public List<IFilterable> GetSelectOptions(IEnumerable<Product> products)
        {
            List<IFilterable> countries = new List<IFilterable>();

            foreach (var product in products)
            {
                if (!countries.Contains(product.Country)) countries.Add(product.Country);
            }

            return countries;
        }
    }
}