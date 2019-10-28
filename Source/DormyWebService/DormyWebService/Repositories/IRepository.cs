using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DormyWebService.Repositories
{
    //Generic repository interface
    public interface IRepository<T> where T : class
    {
        Task<ICollection<T>> FindAllAsync();
        Task<T> FindByIdAsync(int id);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        //Find all according to expression
        Task<ICollection<T>> FindAllAsyncWithCondition(Expression<Func<T, bool>> match);
        Task<T> CreateAsync(T entity);
        T CreateAsyncWithoutSave(T entity);
        T CreateWithoutSave(T t);
        Task<T> UpdateAsync(T entity, object key);
        Task<T> UpdateAsyncWithoutSave(T entity, object key);
        Task<int> DeleteAsync(T entity);
    }
}