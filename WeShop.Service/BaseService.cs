using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WeShop.IRepositoty;
using WeShop.IService;

namespace WeShop.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        private readonly IBaseRepositoty<TEntity> _baseRepositoty;

        public BaseService(IBaseRepositoty<TEntity> baseRepositoty)
        {
            _baseRepositoty = baseRepositoty;
        }

        public bool Add(TEntity tEntity)
        {
            _baseRepositoty.Insert(tEntity);
            return _baseRepositoty.SaveChanges();
        }

        public int GetCount(Func<TEntity, bool> whereLambda)
        {
            return _baseRepositoty.QueryCount(whereLambda);
        }

        public IEnumerable<TEntity> GetEntities(Func<TEntity, bool> whereLambda)
        {
            return _baseRepositoty.QueryEntities(whereLambda);
        }

        public IEnumerable<TEntity> GetEntitiesByPage<TType>(int pageSize, int pageIndex, bool isAsc,
            Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TType>> orderByLambda)
        {
            return _baseRepositoty.QueryEntitiesByPage(pageSize, pageIndex, isAsc, whereLambda, orderByLambda);
        }

        public TEntity GetEntity(Func<TEntity, bool> whereLambda)
        {
            return _baseRepositoty.QueryEntity(whereLambda);
        }

        public bool Modify(TEntity tEntity)
        {
            _baseRepositoty.Update(tEntity);
            return _baseRepositoty.SaveChanges();
        }

        public bool Remove(TEntity tEntity)
        {
            _baseRepositoty.Delete(tEntity);
            return _baseRepositoty.SaveChanges();
        }
    }
}