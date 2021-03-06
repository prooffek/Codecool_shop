using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Models;
using Microsoft.Extensions.Configuration;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class TravelAgencyDaoMemory : ITravelAgencyDao
    {
        private ShopContext _shopContext;
        private List<TravelAgency> data = new List<TravelAgency>();
        private static TravelAgencyDaoMemory instance = null;

        public TravelAgencyDaoMemory(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public TravelAgencyDaoMemory()
        {
            _shopContext = new ShopContext();
        }

        public static TravelAgencyDaoMemory GetInstance()
        {
            if (instance == null)
            {
                instance = new TravelAgencyDaoMemory();
            }

            return instance;
        }

        public void Add(TravelAgency item)
        {
            _shopContext.Add(item);
            _shopContext.SaveChanges();
            /*
            item.Id = data.Count + 1;
            data.Add(item);
            */
        }

        public void Remove(int id)
        {
            TravelAgency travelAgencyToRemove = Get(id);
            _shopContext.TravelAgency.Remove(travelAgencyToRemove);
            _shopContext.SaveChanges();
            //data.Remove(this.Get(id));
        }

        public TravelAgency Get(int id)
        {
            var agency = _shopContext.TravelAgency.Find(id);
            return _shopContext.TravelAgency.Find(id);
            //return data.Find(x => x.Id == id);
        }

        public IEnumerable<TravelAgency> GetAll()
        {
            return _shopContext.TravelAgency.OrderBy(ta => ta.Name);
            //return data;
        }
    }
}
