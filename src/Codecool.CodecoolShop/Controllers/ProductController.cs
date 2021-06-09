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
        public CategoryService CategoryService { get; set; }
        public TravelAgencyService TravelAgencyService { get; set; }
        public FilterServices FilterServices { get; set; }
        public CartService CartService { get; set; }
        public CountryService CountryService { get; set; }
        
        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            ProductService = new ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance(),
                CountryDaoMemory.GetInstance());
            CartService = new CartService();
            TravelAgencyService = new TravelAgencyService(
                ProductDaoMemory.GetInstance(),
                TravelAgencyDaoMemory.GetInstance(),
                ProductService);
            CategoryService = new CategoryService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance(),
                ProductService
            );
            CountryService = new CountryService(ProductDaoMemory.GetInstance(),
                CountryDaoMemory.GetInstance(),
                ProductService);
            FilterServices = new FilterServices(TravelAgencyService, ProductService, CountryService, CategoryService);
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

        public IActionResult Filter(ShopModel shopModel)
        {
            shopModel = FilterServices.Filter(shopModel);
            return View("Index", shopModel);
        }
        
        /*
        public IActionResult FilteredByTravelAgency(ShopModel shopModel)
        {
            return View("Index", TravelAgencyService.FilteredByTravelAgency(shopModel));
        }

        public IActionResult FilteredByCategory(ShopModel shopModel)
        {
            bool anOptionIsSelected = shopModel.ProductCategoryId != 0;

            if (anOptionIsSelected)
            {
                var productsFromCategory = CategoryService.GetProductsForCategory(shopModel.ProductCategoryId);
                shopModel.ConfigureClassPropertiesCategory(ProductService, productsFromCategory);
                return View("Index", shopModel);
            }

            FilteredByTravelAgency(shopModel);
            return View("Index", new ShopModel(ProductService));
        }
        */

        public IActionResult TravelDetails(int id)
        {
            Product product = ProductService.GetProductForId(id);
            return View(product);
        }

        public string GetProductData(int id) //JavaScript
        {
            Product product = ProductService.GetProductForId(id);
            return JsonSerializer.Serialize(product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        /*
        public IActionResult FilteredByCountries(ShopModel shopModel)
        {
            return View("Index", CountryService.FilteredByCountry(shopModel));

        }
        */
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        [Route("/cart")]
        public IActionResult ReviewCart()
        {
            var cart = SessionHelper.GetObjectFromJson<Cart>(HttpContext.Session, "cart") ?? new Cart();
            cart.CountSum();
            return View("Cart", cart);
        }

        public void AddToCart(int id)
        {
            Cart cart = SessionHelper.GetObjectFromJson<Cart>(HttpContext.Session, "cart");
            cart = CartService.AddToCart(id, ProductService, cart);
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
                var cart = SessionHelper.GetObjectFromJson<Cart>(HttpContext.Session, "cart");
                if (cart == null) cart = new Cart();
                cart.CountSum();
                return View("OrderConfirmation", cart);
            }

            return View("FalseOrder");
        }
    }
}
