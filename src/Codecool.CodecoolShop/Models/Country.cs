using System.Collections.Generic;
using Codecool.CodecoolShop.Search;

namespace Codecool.CodecoolShop.Models
{
    public class Country: BaseModel, IFilterable
    {
        public List<Product> Products { get; set; }
        public List<IFilterable> GetSelectOptions(IEnumerable<Product> products)
        {
            var countriesNames = new List<string>();
            List<IFilterable> countries = new List<IFilterable>();

            foreach (var product in products)
            {
                if (!countriesNames.Contains(product.Country.Name))
                {
                    countriesNames.Add((product.Country.Name));
                    countries.Add(product.Country);
                }
            }

            return countries;
        }
    }
}