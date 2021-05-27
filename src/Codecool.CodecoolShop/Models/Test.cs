using System.Collections.Generic;
using Codecool.CodecoolShop.Search;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Codecool.CodecoolShop.Models
{
    public class Test
    {
        public IEnumerable<Product> Products { get; set; }
        public List<IFilterable> AgenciesOptions { get; private set; }
        public SelectList TravelAgenciesList { get; private set; }
        public int TravelAgencyId { get; set; }

        public Test()
        {
        }

        public Test(ProductService productService, int agencyId = 0)
        {
            Products = productService.GetProductsForCategory(1);
            AgenciesOptions = new TravelAgency().GetSelectOptions(Products);
            TravelAgenciesList = new SelectList(AgenciesOptions, "Id", "Name");
            TravelAgencyId = agencyId;
        }

        public void ConfigureClassProperties(ProductService productService)
        {
            Products = productService.GetProductsForCategory(1);
            AgenciesOptions = new TravelAgency().GetSelectOptions(Products);
            TravelAgenciesList = new SelectList(AgenciesOptions, "Id", "Name");
        }
        
        public void ConfigureClassProperties(ProductService productService, IEnumerable<Product> selectedProducts)
        {
            Products = selectedProducts;
            var allProducts = productService.GetProductsForCategory(1);
            AgenciesOptions = new TravelAgency().GetSelectOptions(allProducts);
            TravelAgenciesList = new SelectList(AgenciesOptions, "Id", "Name");
        }
    }
}