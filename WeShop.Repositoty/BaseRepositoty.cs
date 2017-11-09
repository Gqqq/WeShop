using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using WeShop.EFModel;
using WeShop.IRepositoty;

namespace WeShop.Repositoty
{
    public class BaseRepositoty<TEntity> : IBaseRepositoty<TEntity> where TEntity : class, new()
    {
        private WeShopDb _dbContext=DbContextFactory.GetIntance();
        private DbSet<TEntity> _dbSet;

        public BaseRepositoty()
        {
            _dbSet = _dbContext.Set<TEntity>();
        }

        public void Delete(TEntity tEntity)
        {
            _dbSet.Attach(tEntity);
            _dbSet.Remove(tEntity);
        }

        public void Insert(TEntity tEntity)
        {
            _dbSet.AddOrUpdate(tEntity);
        }

        public TEntity QueryEntity(Func<TEntity, bool> whereLambda)
        {
           return _dbSet.FirstOrDefault(whereLambda);
        }

        public int QueryCount(Func<TEntity, bool> whereLambda)
        {
           return _dbSet.Count(whereLambda);
        }

        public IEnumerable<TEntity> QueryEntities(Func<TEntity, bool> whereLambda)
        {
            return _dbSet.Where(whereLambda);
        }

        public IEnumerable<TEntity> QueryEntitiesByPage<TType>(int pageSize, int pageIndex, bool isAsc,
           Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TType>> orderByLambda)
        {
            //生成查询语句
            var result = _dbSet.Where(whereLambda);
            //附加排序
            result = isAsc ? result.OrderBy(orderByLambda) : result.OrderByDescending(orderByLambda);
            //附加分页
            var offset = (pageIndex - 1)*pageSize;
            result = result.Skip(offset).Take(pageSize);
            return result;
        }

        public bool SaveChanges()
        {
            return _dbContext.SaveChanges() > 0;
        }

        public void Update(TEntity tEntity)
        {
            _dbSet.Attach(tEntity);
            //更改实体的状态为 已修改
            _dbContext.Entry(tEntity).State = EntityState.Modified;
        }
    }
}