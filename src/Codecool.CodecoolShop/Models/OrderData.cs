using System;

namespace Codecool.CodecoolShop.Models
{
    public class OrderData
    {
        public int Id { get; set; }
        public DateTime DateOrder { get; set; }
        public Cart Cart { get; set; }
        public UserData UserData { get; set; }
    }
}