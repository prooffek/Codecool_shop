using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class CountryDaoMemory: ICountryDao
    {
        private List<Country> data = new List<Country>();
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
            item.Id = data.Count + 1;
            data.Add(item);
        }

        public void Remove(int id)
        {
            data.Remove(this.Get(id));
        }

        public Country Get(int id)
        {
            return data.Find(x => x.Id == id);
        }

        public IEnumerable<Country> GetAll()
        {
            return data;
        }

        public List<string> GetOptions()
        {
            return data.Select(country => country.Name).ToList();
        }
    }
}