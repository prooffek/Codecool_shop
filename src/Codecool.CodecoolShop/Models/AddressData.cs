using System.ComponentModel.DataAnnotations.Schema;

namespace Codecool.CodecoolShop.Models
{
    public class AddressData
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }

        // navigation property
        public UserData User { get; set; }

    }
}