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

        public virtual async Task<T> FindByIdAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await Context.Set<T>().SingleOrDefaultAsync(match);
        }

        public virtual async Task<ICollection<T>> FindAllAsyncWithCondition(Expression<Func<T, bool>> match)
        {
            return await Context.Set<T>().Where(match).ToListAsync();
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            Context.Set<T>().Add(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity, object key)
        {
            if (entity == null)
                return null;
            T exist = await Context.Set<T>().FindAsync(key);
            if (exist == null) return null;
            Context.Entry(exist).CurrentValues.SetValues(entity);
            await Context.SaveChangesAsync();
            return exist;
        }

        public virtual async Task<T> UpdateAsyncWithoutSave(T entity, object key)
        {
            if (entity == null)
                return null;
            T exist = await Context.Set<T>().FindAsync(key);
            if (exist == null) return null;
            Context.Entry(exist).CurrentValues.SetValues(entity);
            return exist;
        }

        public virtual async Task<int> DeleteAsync(T entity)
        {
            Context.Set<T>().Remove(entity);
            return await Context.SaveChangesAsync();
        }
    }
}