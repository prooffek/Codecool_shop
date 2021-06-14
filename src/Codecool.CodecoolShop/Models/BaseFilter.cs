using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Services;

namespace Codecool.CodecoolShop.Models
{
    public abstract class BaseFilter : BaseModel
    {
        public List<Product> GetProductForFilter(IService service, int id, List<Product> initialList)
        {
            var newList = service.GetProductsForId(id).ToList();

            if (initialList.Count == 0)
                return newList;
            var result = newList.Where(product => initialList.Contains(product)).ToList();
            return result;
        }
        
        public List<Product> GetProductForFilter(int id, List<Product> initialList)
        {
            var newList = service.GetProductsForId(id).ToList();

            if (initialList.Count == 0)
                return newList;
            var result = newList.Where(product => initialList.Contains(product)).ToList();
            return result;
        }
    }
}