using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    /// <summary>
    /// To build the repository, it will be necessary a super class containing the generic methods used by the DBSet object.
    /// 关于EF异步，可以参考： https://www.c-sharpcorner.com/article/net-entity-framework-core-generic-async-operations-with-unit-of-work-generic-re/
    /// 以及 Nuget System.Linq.Async
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T:class
    {
        IEnumerable<T> List(Expression<Func<T, bool>> expression);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        bool Any(Expression<Func<T, bool>> expression);

        T Save(T entity);

        T Update(T entity);

        void Delete(T entity);

        Task AddAsync(T entity);
    }
}
