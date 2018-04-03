using System;
using System.Collections.Generic;

namespace CustomerServiceAPI.Services
{
    public interface IRepository<T>
    {
        IEnumerable<T> FetchAll();
        T Query(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        bool Save();
    }
}
