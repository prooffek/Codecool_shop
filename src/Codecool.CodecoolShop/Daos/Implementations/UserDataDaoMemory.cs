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
            /*
            var user = new UserData();
            var query =
                from usr in _shopContext.User
                join adr in _shopContext.AddressData
                    on Convert.ToInt32(usr.AddressData) equals adr.Id
                select new
                {
                    UserId = usr.Id,
                    FirstName = usr.FirstName,
                    LastName = usr.LastName,
                    Password = usr.Password,
                    Email = usr.Email,
                    PhoneNumber = usr.PhoneNumber,
                    AddressId = adr.Id,
                    Country = adr.Country,
                    City = adr.City,
                    ZipCode = adr.ZipCode,
                    Street = adr.Street
                };

            foreach (var usr in query)
            {
                user.Id = usr.UserId;
                user.FirstName = usr.FirstName;
                user.LastName = usr.LastName;
                user.Password = usr.Password;
                user.Email = usr.Email;
                user.PhoneNumber = usr.PhoneNumber;
                user.AddressData.Id = usr.AddressId;
                user.AddressData.Country = usr.Country;
                user.AddressData.City = usr.City;
                user.AddressData.ZipCode = usr.ZipCode;
                user.AddressData.Street = usr.Street;
            }
            */

            var user = _shopContext.User.Find(id);
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