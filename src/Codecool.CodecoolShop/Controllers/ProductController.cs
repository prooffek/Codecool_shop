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
using Microsoft.AspNetCore.Http;
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
                ProductCategoryDaoMemory.GetInstance(),
                CountryDaoMemory.GetInstance());
            TravelAgencyService = new TravelAgencyService(
                ProductDaoMemory.GetInstance(),
                TravelAgencyDaoMemory.GetInstance());
        }

        public IActionResult Index()
        {
            var shopModel = new ShopModel(ProductService);
            // var cart = SessionHelper.GetObjectFromJson<Cart>(HttpContext.Session, "cart");

            return View(shopModel);
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
            var cart = SessionHelper.GetObjectFromJson<Cart>(HttpContext.Session, "cart");
            if (cart == null) cart = new Cart();
            cart.CountSum();
            return View("Cart", cart);
        }

        public void AddToCart(int id)
        {
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
        }

        public IActionResult DecreaseProductsQuantity(int index)
        {
            var cart = SessionHelper.GetObjectFromJson<Cart>(HttpContext.Session, "cart");
            CartItem cartItem = cart.CartItems[index];
            cart.DecrementCartItemQuantity(cartItem);
            cartItem.CountSum();
            cart.CountSum();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return View("Cart", cart);
        }

        public IActionResult IncreaseProductsQuantity(int index)
        {
            var cart = SessionHelper.GetObjectFromJson<Cart>(HttpContext.Session, "cart");
            CartItem cartItem = cart.CartItems[index];
            cart.IncrementCartItemQuantity(cartItem);
            cartItem.CountSum();
            cart.CountSum();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return View("Cart", cart);
        }

        public IActionResult RemoveProductFromCart(int index)
        {
            var cart = SessionHelper.GetObjectFromJson<Cart>(HttpContext.Session, "cart");
            cart.CartItems.RemoveAt(index);
            cart.CountSum();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return View("Cart", cart);
        }

        public IActionResult Checkout()
        {
            var checkout = SessionHelper.GetObjectFromJson<Checkout>(HttpContext.Session, "checkout") ?? new Checkout();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "checkout", checkout);
            
            return View("Checkout", checkout);
        }

        public void SaveCheckoutData(Checkout checkout)
        {
            SessionHelper.SetObjectAsJson(HttpContext.Session, "checkout", checkout);
        }

        [Route("/payment")]
        public IActionResult Payment(Checkout checkout)
        {
            SaveCheckoutData(checkout);
            
            var cart = SessionHelper.GetObjectFromJson<Cart>(HttpContext.Session, "cart");
            if (cart.Sum == 0)
            {
                Payment payment = new Payment();
                payment.CardHolder = checkout.Name ?? "Jan Kowalski";
                return View("Payment", payment);
            }
            else
            {
                Payment payment = new Payment(cart.Sum);
                payment.CardHolder = checkout.Name ?? "Jan Kowalski";
                return View("Payment", payment);
            }
            
        }

        public IActionResult CheckPayment(Payment payment)
        {
            if (payment.IsDataCorrect())
            {
                return View("CorrectOrder");
            }
            return View("FalseOrder");
        }
    }
}
