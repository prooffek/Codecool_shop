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

        public void EditCartItemQuantity(CartItem cartItem, int newQuantity)
        {
            if (CartItems.Contains(cartItem) && newQuantity > 0) 
                cartItem.EditQuantity(newQuantity);
            else if (CartItems.Contains(cartItem)) //newQuantity is below zero
                RemoveCartItem(cartItem);
        }
    }
}