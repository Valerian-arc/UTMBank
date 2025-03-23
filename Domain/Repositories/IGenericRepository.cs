using Domain.Entites;
using System.Collections.Generic;

namespace Domain.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        void Add(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Update(T entity);
        void Delete(int id);
    }
}
