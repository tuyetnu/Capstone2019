using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DormyWebService.Entities;
using Microsoft.EntityFrameworkCore;

namespace DormyWebService.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected DormyDbContext Context;

        protected RepositoryBase(DormyDbContext context)
        {
            Context = context;
        }

        public virtual async Task<ICollection<T>> FindAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public virtual Task<T> FindByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public virtual async Task<ICollection<T>> FindAllAsyncWithCondition(Expression<Func<T, bool>> match)
        {
            return await Context.Set<T>().Where(match).ToListAsync();
        }

        public virtual Task CreateAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task UpdateAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

        public virtual async Task<int> DeleteAsync(T entity)
        {
            Context.Set<T>().Remove(entity);
            return await Context.SaveChangesAsync();
        }
    }
}