using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.Json;
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

        //public Cart cart = new Cart();
        
        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            ProductService = new ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance(),
                CountryDaoMemory.GetInstance());
            TravelAgencyService = new TravelAgencyService(
                ProductDaoMemory.GetInstance(),
                TravelAgencyDaoMemory.GetInstance());
        }

        public IActionResult Index()
        {
            var shopModel = new ShopModel(ProductService);
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            //var products = ProductService.GetProductsForCategory(1);
            //ShopModel shopModel= new ShopModel() {ProductsList = products.ToList()};
            //FillListsToFilter(shopModel, products);
            
            return View(shopModel);
        }

        private void FillListsToFilter(ShopModel shopModel, IEnumerable<Product> products )
        {
            // shopModel.CountriesList = GetNamesToPrint(products, "Country");// do zmiany kiedy będzie object country
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
        //  private List<string> GetObjectsToFilterCountry(IEnumerable<Product> products)
        // {
        //     List<Country> countryList = new List<string>();
        //     
        //     foreach (var element in products)
        //     {
        //         var propertyValue = element.GetType().GetProperty(name).GetValue(element).ToString();
        //         if(!NamesList.Contains(propertyValue))
        //         {
        //             NamesList.Add(propertyValue);
        //         }
        //     }
        //
        //     return NamesList;
        // }


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

        public IActionResult FilteredByCategory(ShopModel shopModel)
        {
            bool anOptionIsSelected = shopModel.ProductCategoryId != 0;

            if (anOptionIsSelected)
            {

            var productsFromCategory = ProductService.GetProductsForCategory(shopModel.ProductCategoryId);
            shopModel.ConfigureClassPropertiesCategory(ProductService, productsFromCategory);
            return View("Index", shopModel);
            }

            FilteredByTravelAgency(shopModel);
            return View("Index", new ShopModel(ProductService));
        }

        public IActionResult TravelDetails(int id)
        {
            Product product = ProductService.GetProductForId(id);
            return View(product);
        }

        public string GetProductData(int id)
        {
            Product product = ProductService.GetProductForId(id);
            return JsonSerializer.Serialize(product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult FilteredByCountries(ShopModel shopModel)
        {
            int selectedValue = shopModel.CountryId;

            if (selectedValue != 0)
            {
                var productsFromCountry = ProductService.GetProductsForCountry(shopModel.CountryId);
                shopModel.ConfigureClassProperties(ProductService, productsFromCountry);
                return View("Index", shopModel);
            }

            return View("Index", new ShopModel(ProductService));

        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        [Route("/cart")]
        public IActionResult ReviewCart()
        {
            return View("Cart");
        }

        public void AddToCart(int id)
        {
            Console.WriteLine(id);
            Cart cart = SessionHelper.GetObjectFromJson<Cart>(HttpContext.Session, "cart");
            Product product = ProductService.GetProductForId(id);
            CartItem cartItem = new CartItem(product, 1);
            if (cart == null)
            {
                cart = new Cart();
                cart.CartItems = new List<CartItem>();
            }
            cart.CartItems.Add(cartItem);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            Console.WriteLine(cart.CartItems.Count());
            foreach (var element in cart.CartItems)
            {
                Console.WriteLine(element.Product.Name);
            }
            
        }
    }
}
