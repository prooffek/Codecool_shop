using System.Collections.Generic;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class AddressDataDaoMemory : IAddressDataDao
    {
        private ShopContext _shopContext;
        public AddressDataDaoMemory()
        {
            _shopContext = new ShopContext();
        }
        public void Add(AddressData item)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public AddressData Get(int id)
        {
            return _shopContext.AddressData.Find(id);
            throw new System.NotImplementedException();
        }

        public IEnumerable<AddressData> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}