using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Northwind.DataAccess.Abstract;

namespace Northwind.DataAccess.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity> 
        where TEntity:class,new()
        where TContext:DbContext,new()
    {
    public void Add(TEntity entity)
    {
        using (TContext context =new TContext())
        {
            var addedEentity = context.Entry(entity);
            addedEentity.State = EntityState.Added;
            context.SaveChanges();
        }
            
    }

    public void Delete(TEntity entity)
    {
        using (TContext context = new TContext())
        {
            var deletedEentity = context.Entry(entity);
            deletedEentity.State = EntityState.Deleted;
            context.SaveChanges();
        }

        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
    {
        using (TContext context =new TContext())
        {
            return context.Set<TEntity>().SingleOrDefault(filter);
        }
    }

    public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
    {
        using (TContext context =new TContext())
        {
            return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
        }
        
    }

    public void Update(TEntity entity)
    {
        using (TContext context = new TContext())
        {
            var updatedEentity = context.Entry(entity);
            updatedEentity.State = EntityState.Modified;
            context.SaveChanges();
        }
        }
    }
}
