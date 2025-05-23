using Application.IRepository;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class AppRepository<T> : IAppRepository<T> where T : class
    {  
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
          public AppRepository(AppDbContext context) 
        { 
          _context = context;
            _dbSet = _context.Set<T>();
        }

        #region
        public async Task<IEnumerable<T>> GetAllAsync()
        {
           var result = await _dbSet.ToListAsync();
            _context.SaveChanges();
            return result;
        }

        public async Task<T> GetById(object id)
        {
           var result = await _dbSet.FindAsync(id);
            _context.SaveChanges();
            return await Exists(id) ? result : null;
        }

        public async Task<T> Insertasync(T entity)
        {
            await _dbSet.AddAsync(entity);
           await _context.SaveChangesAsync();
            return entity;
            
        }

        public async Task<T> RemoveAsync(T entity)
        {
           _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;

        }

        //public async Task SaveAsync()
        //{
        //   await _context.SaveChangesAsync();
        //}

        public async Task<T> UpdateAsync(T entity)
        {
             _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

     public async Task<bool> Exists(object  id)
        {
            var result = await _dbSet.FindAsync(id);
            if(result != null)
            {
                return true;
            }
            return false;
        }

        public async  Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _dbSet;
            if(navigationProperties is not null)
            {
                foreach(var navigationProperty in navigationProperties)
                    query =query.Include(navigationProperty);
            }
            return await query.Where(predicate).ToListAsync();
            
               
        }

       
        #endregion
    }

}
