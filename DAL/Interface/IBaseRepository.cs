using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DAL.Interface
{
    /// <summary>
    /// To build the repository, it will be necessary a super class containing the generic methods used by the DBSet object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T:class
    {
        IEnumerable<T> List(Expression<Func<T, bool>> expression);

        bool Any(Expression<Func<T, bool>> expression);

        T Save(T entity);

        T Update(T entity);

        void Delete(T entity);
    }
}
