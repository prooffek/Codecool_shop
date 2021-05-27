using System.Collections.Generic;
using Codecool.CodecoolShop.Controllers;
using Codecool.CodecoolShop.Services;

namespace Codecool.CodecoolShop.Models
{
    public class ShopModel
    {
        public List<Codecool.CodecoolShop.Models.Product> ProductsList;
        public List<string> CountriesList { get; set; }
        public List<TravelAgency> TravelAgenciesList { get; set; }
        public List<ProductCategory> CategoriesList { get; set; }
        
     
        
    }
}