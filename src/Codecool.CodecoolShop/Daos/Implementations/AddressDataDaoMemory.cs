﻿using System.Collections.Generic;
using Codecool.CodecoolShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class AddressDataDaoMemory : IAddressDataDao
    {
        private ShopContext _shopContext = new ShopContext();
        
        public void Add(AddressData item)
        {
            _shopContext.AddressDatas.Add(item);
            _shopContext.SaveChanges();
        }

        public void Remove(int id)
        {
            var addressData = _shopContext.AddressDatas.First(address => address.Id == id);
            _shopContext.AddressDatas.Remove(addressData);
            _shopContext.SaveChanges();
        }

        public AddressData Get(int id)
        {
            return _shopContext.AddressDatas.FirstOrDefault(adress => adress.Id == id);
            // var address = _shopContext.AddressData.Find(id);
            // return address;
            //throw new System.NotImplementedException();
        }

        public IEnumerable<AddressData> GetAll()
        { 
            return _shopContext.AddressDatas.ToList();
        }
    }
}