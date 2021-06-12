using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class CategoryService : IService
    {
        private readonly IProductCategoryDao _productCategoryDao;
        private readonly IProductDao _productDao;
        private ProductService _productService;

        public CategoryService(IProductDao productDao,IProductCategoryDao productCategoryDao,ProductService productService)
        {
            this._productCategoryDao = productCategoryDao;
            this._productDao = productDao;
            this._productService = productService;
        }

        public ProductCategory GetProductCategory(int categoryId)
                {
                    return this._productCategoryDao.Get(categoryId);
                }
        
        public IEnumerable<Product> GetProductsForId(int categoryId)
        {
            ProductCategory category = this._productCategoryDao.Get(categoryId);
            return this._productDao.GetBy(category);
        }
    }
}