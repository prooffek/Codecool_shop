using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Controllers;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Search;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Codecool.CodecoolShop.Models
{
    public class ShopModel
    {
        public IEnumerable<Product> Products { get; set; }
        public List<IFilterable> AgenciesOptions { get; private set; }
        public SelectList TravelAgencies { get; private set; }
        public int TravelAgencyId { get; set; }
        public SelectList Countries { get; private set; }
        public int CountryId { get; set; }
        public int ProductCategoryId { get; set; }
        public SelectList Categories { get; set; }
        public List<IFilterable> CategoriesOptions { get; set; }
        public List<IFilterable> CountriesOptions { get; set; }

        private ProductDaoMemory _productContext;

        public ShopModel()
        {
        }
        
        public ShopModel(ProductDaoMemory productDaoMemory)
        {
            _productContext = productDaoMemory;
            ConfigureShopModelProperties();
        }

        private void ConfigureShopModelProperties()
        {
            Products = _productContext.GetAll().ToList();
            ConfigureOptionsLists(Products);
            ConfigureSelectLists();
            SetDropDownIds();
        }
        
        public void ConfigureShopModelProperties(ProductDaoMemory productContext, IEnumerable<Product> selectedProducts)
        {
            _productContext = productContext;
            Products = selectedProducts;
            var allProducts = _productContext.GetAll().ToList();
            ConfigureOptionsLists(allProducts);
            ConfigureSelectLists();
        }
        
        private void ConfigureOptionsLists(IEnumerable<Product> products)
        {
            AgenciesOptions = new TravelAgency().GetSelectOptions(products);
            CategoriesOptions = new ProductCategory().GetSelectOptions(products);
            CountriesOptions = new Country().GetSelectOptions(products);
        }

        private void ConfigureSelectLists()
        {
            TravelAgencies = new SelectList(AgenciesOptions, "Id", "Name");
            Categories = new SelectList(CategoriesOptions, "Id", "Name");
            Countries = new SelectList(CountriesOptions, "Id", "Name");
        }

        private void SetDropDownIds()
        {
            TravelAgencyId = 0;
            CountryId = 0;
            ProductCategoryId = 0;
        }

        /*
        public ShopModel(ProductService productService, int agencyId = 0, int countryId = 0, int categoryId = 0)
        {
            Products = productService.GetAllProducts();
            AllProducts = productService.GetAllProducts();
            AgenciesOptions = new TravelAgency().GetSelectOptions(Products);
            CategoriesOptions = new ProductCategory().GetSelectOptions(AllProducts);
            TravelAgencies = new SelectList(AgenciesOptions, "Id", "Name");
            Categories = new SelectList(CategoriesOptions, "Id", "Name");
            TravelAgencyId = agencyId;
            CountriesOptions = new Country().GetSelectOptions(Products);
            Countries = new SelectList(CountriesOptions, "Id", "Name");
            CountryId = countryId;
            ProductCategoryId = categoryId;
        }
        */

        /*
        public void ConfigureClassProperties(ProductService productService, IEnumerable<Product> selectedProducts)
        {
            Products = selectedProducts;
            var allProducts = productService.GetAllProducts(); //prop
            AgenciesOptions = new TravelAgency().GetSelectOptions(allProducts); //prop
            TravelAgencies = new SelectList(AgenciesOptions, "Id", "Name");
            CountriesOptions = new Country().GetSelectOptions(Products); //prop
            Countries = new SelectList(CountriesOptions, "Id", "Name"); //prop
            CategoriesOptions = new ProductCategory().GetSelectOptions(Products); //prop
            Categories = new SelectList(CategoriesOptions, "Id", "Name");
        }
        */
        
        /*
        public void ConfigureClassPropertiesCategory(ProductService productService, IEnumerable<Product> selectedProducts)
        {
            Products = selectedProducts;
            AgenciesOptions = new TravelAgency().GetSelectOptions(Products);
            TravelAgencies = new SelectList(AgenciesOptions, "Id", "Name");
            CategoriesOptions = new ProductCategory().GetSelectOptions(Products);
            Categories = new SelectList(CategoriesOptions, "Id", "Name");
            CountriesOptions = new Country().GetSelectOptions(Products);
            Countries = new SelectList(CountriesOptions, "Id", "Name");
        }
        */
    }
}