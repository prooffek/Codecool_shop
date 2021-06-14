using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface IDao<T> : IDaoMemory
    {
        void Add(T item);
        void Remove(int id);

        T Get(int id);
        IEnumerable<T> GetAll();
    }
}
