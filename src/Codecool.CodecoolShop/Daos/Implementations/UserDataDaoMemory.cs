using System;
using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class UserDataDaoMemory : IUserDataDao
    {
        private ShopContext _shopContext;

        public UserDataDaoMemory()
        {
            _shopContext = new ShopContext();
        }
        
        public void Add(UserData item)
        {
            _shopContext.Add(item);
            _shopContext.SaveChanges();
        }

        public void Remove(int id)
        {
            UserData userToRemove = Get(id);
            _shopContext.Remove(userToRemove);
            _shopContext.SaveChanges();
            //throw new System.NotImplementedException();
        }

        public UserData Get(int id)
        {
            var user = _shopContext.User.Find(id);
            int addressId = user.AddressDataId;
            user.AddressData = new AddressDataDaoMemory().Get(addressId);
            return user;
            //throw new System.NotImplementedException();
        }

        public IEnumerable<UserData> GetAll()
        {
            return _shopContext.User.OrderBy(user => user.Id);
            //throw new System.NotImplementedException();
        }
    }
}