using DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private DbSet<T> entity_;
        protected DBContext context;

        public BaseRepository(DBContext context)
        {
            this.context = context;
            entity_ = context.Set<T>();
        }

        //public Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        //   => context.Set<T>().FirstOrDefaultAsync(predicate);

        public IEnumerable<T> List(Expression<Func<T, bool>> expression)
        {
            return entity_.Where(expression).ToList();
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return entity_.Any(expression);
        }

        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        /*https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members*/
        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate) 
            => context.Set<T>().FirstOrDefaultAsync(predicate);


        public T Save(T entity)
        {
            entity_.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            entity_.Update(entity);
            context.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            entity_.Remove(entity);
            context.SaveChanges();
        }
    }
}
