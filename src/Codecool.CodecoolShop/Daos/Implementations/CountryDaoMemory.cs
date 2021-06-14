using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Models;


namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class CountryDaoMemory: ICountryDao
    {
        private ShopContext _shopContext; // = new ShopContext();
        // private List<Country> data = new List<Country>();
        private static CountryDaoMemory instance = null;

        public CountryDaoMemory(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public CountryDaoMemory()
        {
            _shopContext = new ShopContext();
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
            _shopContext.Country.Add(item);
            _shopContext.SaveChanges();
        }

        public void Remove(int id)
        {
            var country = _shopContext.Country.First(countryData => countryData.Id == id);
            _shopContext.Country.Remove(country);
            _shopContext.SaveChanges();
        }

        public Country Get(int id)
        {
            return _shopContext.Country.FirstOrDefault(country => country.Id == id);
        }

        public IEnumerable<Country> GetAll()
        {
            return _shopContext.Country.ToList();
        }
    }
}