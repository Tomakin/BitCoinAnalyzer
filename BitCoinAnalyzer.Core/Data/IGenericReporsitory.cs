using BitCoinAnalyzer.Core.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinAnalyzer.Core.Data
{
    public interface IGenericRepository<T>
         where T : class, IEntity, new()
    {
        T Get(Expression<Func<T, bool>> filter);
        T GetByID(int ID);

        IQueryable<T> GetAll();
        IQueryable<T> GetList(Expression<Func<T, bool>> filter);

        int Count(Expression<Func<T, bool>> filter = null);

        void Add(T entity);
        void Update(T entity);

        void Delete(T entity);
        void DeleteByID(int ID);
    }
}
