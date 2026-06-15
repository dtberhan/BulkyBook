using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repo.IRepo
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T?> Get(Expression<Func<T?, bool>> filter);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
