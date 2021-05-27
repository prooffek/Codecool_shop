using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Search;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        public ProductService ProductService { get; set; }
        public TravelAgencyService TravelAgencyService { get; set; }

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            ProductService = new ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance());
            TravelAgencyService = new TravelAgencyService(
                ProductDaoMemory.GetInstance(),
                TravelAgencyDaoMemory.GetInstance());
        }

        public IActionResult Index()
        {
            var ShopModel = new ShopModel(ProductService);

            return View(ShopModel);
            
            //var products = ProductService.GetProductsForCategory(1);
            //ShopModel shopModel= new ShopModel() {ProductsList = products.ToList()};
            //FillListsToFilter(shopModel, products);
            //return View(shopModel);
        }

        private void FillListsToFilter(ShopModel shopModel, IEnumerable<Product> products )
        {
            shopModel.CountriesList = GetNamesToPrint(products, "Country");
            //shopModel.CategoriesList = TakeNamesToPrint(products, "ProductCategory");
            //shopModel.TravelAgenciesList = TakeNamesToPrint(products, "TravelAgency");
        }

        private List<string> GetNamesToPrint(IEnumerable<Product> products, string name)
        {
            List<string> NamesList = new List<string>();
            
            foreach (var element in products)
            {
                var propertyValue = element.GetType().GetProperty(name).GetValue(element).ToString();
                if(!NamesList.Contains(propertyValue))
                {
                    NamesList.Add(propertyValue);
                }
            }

            return NamesList;
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult FilterByTravelAgency(ShopModel shopModel)
        {
            var selectedProducts = TravelAgencyService.GetProductsForTravelAgencies(shopModel.TravelAgencyId);
            shopModel.ConfigureClassProperties(ProductService, selectedProducts);
            return View("Index", shopModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
