using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Search;
using Codecool.CodecoolShop.Services;

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

        public List<Product> GetProductForFilter(IService service, int id, List<Product> initialList)
        {
            var newList = service.GetProductsForId(id).ToList();

            if (initialList.Count == 0)
                return newList;
            var result = newList.Where(product => initialList.Contains(product)).ToList();
            return result;
        }


        public override string ToString()
        {
            return new string($"Id: {Id} Name: {Name} Description: {Description}");
        }
    }
}
