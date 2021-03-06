using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Search;
using Codecool.CodecoolShop.Services;

namespace Codecool.CodecoolShop.Models
{
    public class ProductCategory: BaseFilter, IFilterable
    {
        
        public List<Product> Products { get; set; }
        public List<IFilterable> GetSelectOptions(IEnumerable<Product> products)
        {
            var categoryNames = new List<string>();
            var productsByCategory = new List<IFilterable>();

            foreach (var product in products)
            {
                if (!categoryNames.Contains(product.ProductCategory.Name))
                {
                    categoryNames.Add(product.ProductCategory.Name);
                    productsByCategory.Add(product.ProductCategory);
                }
            }

            return productsByCategory;
            
            /*
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
            */
        }

        public override string ToString()
        {
            return new string($"Id: {Id} Name: {Name} Description: {Description}");
        }
        
    }
}
