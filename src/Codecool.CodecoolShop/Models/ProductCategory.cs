using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Search;
using Codecool.CodecoolShop.Services;

namespace Codecool.CodecoolShop.Models
{
    public class ProductCategory: BaseModel, IFilterable
    {
        public List<Product> Products { get; set; }
        public List<IFilterable> GetSelectOptions(IEnumerable<Product> products1)
        {
            List<IFilterable> categoriesNames = new List<IFilterable>();
            var count = 0;
            foreach (var product in products1)
            {
                //if (!categoriesNames.Contains(product.ProductCategory)) categoriesNames.Add(product.ProductCategory);
                if (!categoriesNames.Contains(product.ProductCategory))
                {
                    count += 1;
                    categoriesNames.Add(product.ProductCategory);
                }
            }

            return categoriesNames;
        }
        
        public List<Product> GetProductForFilter(IService service, int id, List<Product> initialList)
        {
            var newList = service.GetProductsForId(id).ToList();

            if (initialList.Count == 0)
                return newList;
            return newList.Where(product => initialList.Contains(product)).ToList();
        }
        
        public override string ToString()
        {
            return new string($"Id: {Id} Name: {Name} Description: {Description}");
        }
        
    }
}
