using matxicorp.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace matxicorp.Data.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext _contex;
        public RepositoryBase(ApplicationDbContext context)
        {
            _contex = context;
        }

        public void Add(T entity)
        {
            _contex.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _contex.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return _contex.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _contex.Set<T>().Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            _contex.Set<T>().Update(entity);
        }
    }
}
