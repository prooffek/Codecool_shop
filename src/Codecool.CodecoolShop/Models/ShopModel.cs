using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Controllers;
using Codecool.CodecoolShop.Search;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Codecool.CodecoolShop.Models
{
    public class ShopModel
    {
        public List<Product> ProductsList;
        public List<Country> CountriesList { get; set; }
        public List<TravelAgency> TravelAgenciesList { get; set; }
        public List<ProductCategory> CategoriesList { get; set; }
        
        public IEnumerable<Product> Products { get; set; }
        public List<IFilterable> AgenciesOptions { get; private set; }
        public SelectList TravelAgencies { get; private set; }
        public int TravelAgencyId { get; set; }
        public SelectList Countries { get; private set; }
        public int CountryId { get; set; }
        
        
        
        public ShopModel()
        {
        }

        public ShopModel(ProductService productService, int agencyId = 0, int countryId = 0)
        {
            Products = productService.GetProductsForCategory(1);
            AgenciesOptions = new TravelAgency().GetSelectOptions(Products);
            TravelAgencies = new SelectList(AgenciesOptions, "Id", "Name");
            TravelAgencyId = agencyId;
            CountriesList = productService.GetAllCountries().ToList();
            Countries = new SelectList(CountriesList, "Id", "Name");
            CountryId = countryId;
        }

        public void ConfigureClassProperties(ProductService productService, IEnumerable<Product> selectedProducts)
        {
            Products = selectedProducts;
            var allProducts = productService.GetProductsForCategory(1);
            AgenciesOptions = new TravelAgency().GetSelectOptions(allProducts);
            TravelAgencies = new SelectList(AgenciesOptions, "Id", "Name");
            CountriesList = productService.GetAllCountries().ToList();
            Countries = new SelectList(CountriesList, "Id", "Name");
        }
    }
}