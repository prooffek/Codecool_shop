using System.Collections.Generic;
using System.Linq;

namespace Codecool.CodecoolShop.Models
{
    public class Cart
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public decimal Sum { get; set; }
        public UserData UserData { get; set; }

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

        public void DecrementCartItemQuantity(CartItem cartItem)
        {
            if (CartItems.Contains(cartItem))
                cartItem.DecrementQuantity();

            if (cartItem.Quantity <= 0)
                RemoveCartItem(cartItem);
        }

        public void IncrementCartItemQuantity(CartItem cartItem)
        {
            if (CartItems.Contains(cartItem))
                cartItem.IncrementQuantity();
        }

        public override string ToString()
        {
            string s = $"";
            foreach (var cartItem in CartItems)
            {
                s += $"Nazwa produktu: {cartItem.Product.Name}. Ilość: {cartItem.Quantity} \n";
            }

            s += $"Suma: {this.Sum}";
            return s;
        }
    }
}