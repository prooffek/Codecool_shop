using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Models;
using Data;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class CountryDaoMemory: ICountryDao
    {
        private ShopContext _shopContext = new ShopContext();
        // private List<Country> data = new List<Country>();
        private static CountryDaoMemory instance = null;

        private CountryDaoMemory()
        {
        }

        public static CountryDaoMemory GetInstance()
        {
            if (instance == null)
            {
                instance = new CountryDaoMemory();
            }

            return instance;
        }

        public void Add(Country item)
        {
            _shopContext.Countries.Add(item);
            _shopContext.SaveChanges();
        }

        public void Remove(int id)
        {
            var country = _shopContext.Countries.First(countryData => countryData.Id == id);
            _shopContext.Countries.Remove(country);
            _shopContext.SaveChanges();
        }

        public Country Get(int id)
        {
            return _shopContext.Countries.FirstOrDefault(country => country.Id == id);
        }

        public IEnumerable<Country> GetAll()
        {
            return _shopContext.Countries.ToList();
        }
    }
}