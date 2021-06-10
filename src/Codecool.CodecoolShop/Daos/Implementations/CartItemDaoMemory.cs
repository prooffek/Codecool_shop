using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Models;
using Data;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class CartItemDaoMemory : ICartItemDao
    {
        private ShopContext _shopContext = new ShopContext();
        public void Add(CartItem item)
        {
            _shopContext.CartItems.Add(item);
            _shopContext.SaveChanges();
        }

        public void Remove(int id)
        {
            var cartItem = _shopContext.CartItems.First(item => item.Id == id);
            _shopContext.CartItems.Remove(cartItem);
            _shopContext.SaveChanges();
        }

        public CartItem Get(int id)
        {
            return _shopContext.CartItems.FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<CartItem> GetAll()
        {
            return _shopContext.CartItems.ToList();
        }
    }
}