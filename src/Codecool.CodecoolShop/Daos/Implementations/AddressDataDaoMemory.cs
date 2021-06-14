using System.Collections.Generic;
using Codecool.CodecoolShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class AddressDataDaoMemory : IAddressDataDao
    {
        private ShopContext _shopContext; // = new ShopContext();

        public AddressDataDaoMemory(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public void Add(AddressData item)
        {
            _shopContext.AddressData.Add(item);
            _shopContext.SaveChanges();
        }

        public void Remove(int id)
        {
            var addressData = _shopContext.AddressData.First(address => address.Id == id);
            _shopContext.AddressData.Remove(addressData);
            _shopContext.SaveChanges();
        }

        public AddressData Get(int id)
        {
            return _shopContext.AddressData.Find(id);
        }

        public IEnumerable<AddressData> GetAll()
        { 
            return _shopContext.AddressData.ToList();
        }
    }
}