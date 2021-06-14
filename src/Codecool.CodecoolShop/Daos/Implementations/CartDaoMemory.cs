using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Models;


namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class CartDaoMemory: ICartDao
    {
        private ShopContext _shopContext; // = new ShopContext();

        public CartDaoMemory(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        public void Add(Cart item)
        {
            _shopContext.Cart.Add(item);
            _shopContext.SaveChanges();
        }

        public void Remove(int id)
        {
            var cart = _shopContext.Cart.First(cart => cart.Id == id);
            _shopContext.Cart.Remove(cart);
            _shopContext.SaveChanges();
        }

        public Cart Get(int id)
        {
            return _shopContext.Cart.FirstOrDefault(cart => cart.Id == id);
        }

        public IEnumerable<Cart> GetAll()
        {
            return _shopContext.Cart.ToList();
        }
    }
}