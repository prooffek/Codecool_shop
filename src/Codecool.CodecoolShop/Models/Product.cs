using System.ComponentModel.DataAnnotations;

namespace Codecool.CodecoolShop.Models
{
    public class Product : BaseModel
    {

        public string Currency { get; set; } = "PLN";
        public decimal DefaultPrice { get; set; }
        public int LengthOfStay { get; set; }
        public Country Country { get; set; }
        public string City { get; set; }
        public string ImgName { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public TravelAgency TravelAgency { get; set; }

        public void SetProductCategory(ProductCategory productCategory)
        {
            ProductCategory = productCategory;
            ProductCategory.Products.Add(this);
        }
    }
}
