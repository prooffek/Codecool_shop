using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Search;
using Codecool.CodecoolShop.Services;

namespace Codecool.CodecoolShop.Models
{
    public class TravelAgency : BaseFilter, IFilterable
    {
        public List<Product> Products { get; set; }
        public List<IFilterable> GetSelectOptions(IEnumerable<Product> products)
        {
            var agenciesNames = new List<string>();
            List<IFilterable> TravelAgencies = new List<IFilterable>();

            foreach (var product in products)
            {
                if (!agenciesNames.Contains(product.TravelAgency.Name))
                {
                    agenciesNames.Add(product.TravelAgency.Name);
                    TravelAgencies.Add(product.TravelAgency);
                }
            }

            return TravelAgencies;
        }

        /*
        public List<Product> GetProductForFilter(IService service, int id, List<Product> initialList)
        {
            var newList = service.GetProductsForId(id).ToList();

            if (initialList.Count == 0)
                return newList;
            var result = newList.Where(product => initialList.Contains(product)).ToList();
            return result;
        }
        */


        public override string ToString()
        {
            return new string($"Id: {Id} Name: {Name} Description: {Description}");
        }
    }
}
