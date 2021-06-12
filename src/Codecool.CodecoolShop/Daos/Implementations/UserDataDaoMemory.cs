using System;
using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class UserDataDaoMemory : IUserDataDao
    {
        private ShopContext _shopContext;

        public UserDataDaoMemory(ShopContext db)
        {
            _shopContext = db;
        }

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
            user.AddressData = _shopContext.AddressData.Find(user.AddressDataId);
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