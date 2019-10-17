using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DormyWebService.Repositories
{
    //Generic repository interface
    public interface IRepository<T>
    {
        Task<ICollection<T>> FindAllAsync();
        Task<T> FindByIdAsync(int id);
        //Find all according to expression
        Task<ICollection<T>> FindAllAsyncWithCondition(Expression<Func<T, bool>> match);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}