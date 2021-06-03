using System.Collections.Generic;
using System.Linq;

namespace Codecool.CodecoolShop.Models
{
    public class Cart
    {
        public List<CartItem> CartItems { get; set; } 
        public decimal Sum { get; set; }
        
        public void AddCartItem(CartItem cartItem)
        {
            CartItems.Add(cartItem);
        }

        public void RemoveCartItem(CartItem cartItem)
        {
            CartItems.Remove(cartItem);
        }

        public void CountSum()
        {
            Sum = CartItems.Sum(x => x.Sum);
        }
    }
}