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
            var shopModel = new ShopModel(ProductService);

            //var products = ProductService.GetProductsForCategory(1);
            //ShopModel shopModel= new ShopModel() {ProductsList = products.ToList()};
            //FillListsToFilter(shopModel, products);
            
            return View(shopModel);
        }

        private void FillListsToFilter(ShopModel shopModel, IEnumerable<Product> products )
        {
            shopModel.CountriesList = GetNamesToPrint(products, "Country");// do zmiany kiedy będzie object country
            shopModel.CategoriesList = GetObjectsToFilterCategories(products);
            shopModel.TravelAgenciesList = GetObjectsToFilterAgency(products);
        }

        private List<TravelAgency> GetObjectsToFilterAgency(IEnumerable<Product> products)
        {
            List<TravelAgency> travelAgenciesList = new List<TravelAgency>();
            foreach (var product in products)
            {
                if (!travelAgenciesList.Contains(product.TravelAgency))
                {
                    travelAgenciesList.Add(product.TravelAgency);
                }
            }

            return travelAgenciesList;
        }

        private List<ProductCategory> GetObjectsToFilterCategories(IEnumerable<Product> products)
        {            
            List<ProductCategory> produtCategoriesList = new List<ProductCategory>();
            foreach (var product in products)
            {
                if (!produtCategoriesList.Contains(product.ProductCategory))
                {
                    produtCategoriesList.Add(product.ProductCategory);
                }
            }

            return produtCategoriesList;
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

        public IActionResult FilteredByTravelAgency(ShopModel shopModel)
        {
            bool anOptionIsSelected = shopModel.TravelAgencyId != 0;
            
            if (anOptionIsSelected)
            {
                var productsFromTheTravelAgency = TravelAgencyService.GetProductsForTravelAgencies(shopModel.TravelAgencyId);
                shopModel.ConfigureClassProperties(ProductService, productsFromTheTravelAgency);
                return View("Index", shopModel);
            }

            return View("Index", new ShopModel(ProductService));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
