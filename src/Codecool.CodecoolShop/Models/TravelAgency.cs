using System.Collections.Generic;
using Codecool.CodecoolShop.Search;

namespace Codecool.CodecoolShop.Models
{
    public class TravelAgency : BaseModel, IFilterable
    {
        public List<Product> Products { get; set; }
        public List<IFilterable> GetSelectOptions(IEnumerable<Product> products)
        {
            List<IFilterable> agencyNames = new List<IFilterable>();

            foreach (var product in products)
            {
                if (!agencyNames.Contains(product.TravelAgency)) agencyNames.Add(product.TravelAgency);
            }

            return agencyNames;
        }
        
        public override string ToString()
        {
            return new string($"Id: {Id} Name: {Name} Description: {Description}");
        }
    }
}
