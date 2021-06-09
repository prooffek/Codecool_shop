namespace Codecool.CodecoolShop.Models
{
    public class Checkout
    {
        public string Name { get; set; }
        
        public AddressData BillingAddress { get; set; } = new AddressData();
        public AddressData ShippingAddress { get; set; } = new AddressData();
        public UserData UserData { get; set; }
    }
}