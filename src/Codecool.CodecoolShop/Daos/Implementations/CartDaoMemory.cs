using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Models;
using Data;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class CartDaoMemory: ICartDao
    {
        private ShopContext _shopContext = new ShopContext();
        public void Add(Cart item)
        {
            _shopContext.Carts.Add(item);
            _shopContext.SaveChanges();
        }

        public void Remove(int id)
        {
            var cart = _shopContext.Carts.First(cart => cart.Id == id);
            _shopContext.Carts.Remove(cart);
            _shopContext.SaveChanges();
        }

        public Cart Get(int id)
        {
            return _shopContext.Carts.FirstOrDefault(cart => cart.Id == id);
        }

        public IEnumerable<Cart> GetAll()
        {
            return _shopContext.Carts.ToList();
        }
    }
}