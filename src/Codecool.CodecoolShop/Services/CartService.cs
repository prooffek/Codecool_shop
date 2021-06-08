using System.Collections.Generic;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace Codecool.CodecoolShop.Services
{
    
    public class CartService
    { 
            public Cart AddToCart(int id, ProductService productService, Cart cart)
            {
                
                Product product = productService.GetProductForId(id);
                CartItem cartItem = new CartItem(product, 1);
                if (cart == null)
                {
                    cart = new Cart();
                    cart.CartItems = new List<CartItem>();
                }
                cart.CartItems.Add(cartItem);
                return cart;
            }

            // public Cart GetCart()
            // {
            //     var cart = SessionHelper.GetObjectFromJson<Cart>(HttpContext.Session, "cart");
            //     if (cart == null) cart = new Cart();
            //     cart.CountSum();
            //     return cart;
            // }
    }
}