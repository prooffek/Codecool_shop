using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            //var products = ProductService.GetProductsForCategory(1);
            //List<IFilterable> agenciesOptions = new TravelAgency().GetSelectOptions(products);
            //ViewBag.Agencies = new SelectList(agenciesOptions, "Id", "Name");
            var ShopModel = new Test(ProductService);

            return View(ShopModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult FilterByTravelAgency(Test shopModel)
        {
            //var products = TravelAgencyService.GetProductsForTravelAgencies(agencyId);
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
