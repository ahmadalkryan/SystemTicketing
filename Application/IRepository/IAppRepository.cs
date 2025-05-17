using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository
{
    public interface IAppRepository<T> where T : class
    {
        // QUERY
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, 
            params Expression<Func<T, object>>[] navigationProperties);

        //Task<IEnumerable<T>> FindWithAllIncludeAsync(Expression<Func<T, bool>> predicate);



        // CRUD
        Task<T> GetById(object ob);
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> UpdateAsync(T entity);
        Task<T> RemoveAsync(T entity);

        Task<T> Insertasync(T entity);

        //Task<int> Save();

        Task<bool> Exists(object id);

    }
}
