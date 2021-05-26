using System.Collections.Generic;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class TravelAgencyDaoMemory : ITravelAgencyDao
    {
        private List<TravelAgency> data = new List<TravelAgency>();
        private static TravelAgencyDaoMemory instance = null;

        private TravelAgencyDaoMemory()
        {
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
            item.Id = data.Count + 1;
            data.Add(item);
        }

        public void Remove(int id)
        {
            data.Remove(this.Get(id));
        }

        public TravelAgency Get(int id)
        {
            return data.Find(x => x.Id == id);
        }

        public IEnumerable<TravelAgency> GetAll()
        {
            return data;
        }
    }
}
