using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Data.Entity;
using BestFor.Domain.Entities;

namespace BestFor.Data.Fakes
{
    public abstract class FakeDbSet<TEntity> : DbSet<TEntity>, //IOrderedQueryable<TEntity>, 
        IEnumerable<TEntity> //, IEnumerable, IOrderedQueryable, IQueryable, IQueryable<TEntity>
        //, IAsyncEnumerableAccessor<TEntity>, IInfrastructure<IServiceProvider>
        where TEntity : EntityBase, new()
    {
        #region Private Fields
        private readonly ObservableCollection<TEntity> _items;
     //   private readonly IQueryable _query;
        #endregion Private Fields

        protected FakeDbSet()
        {
            _items = new ObservableCollection<TEntity>();
        //    _query = _items.AsQueryable();
        }

   //      IEnumerator IEnumerable.GetEnumerator() { return _items.GetEnumerator(); }
        public IEnumerator<TEntity> GetEnumerator() { return _items.GetEnumerator(); }

        //public Expression Expression { get { return _query.Expression; } }

        //public Type ElementType { get { return _query.ElementType; } }

        //public IQueryProvider Provider { get { return _query.Provider; } }


        public TEntity Add(TEntity entity)
        {
            _items.Add(entity);
            return entity;
        }


        //public override TEntity Add(TEntity entity, GraphBehavior behavior)
        //{
        //    _items.Add(entity);
        //    return entity;
        //}

        //public override TEntity Remove(TEntity entity)
        //{
        //    _items.Remove(entity);
        //    return entity;
        //}

        //public override TEntity Attach(TEntity entity)
        //{
        //    switch (entity.ObjectState)
        //    {
        //        case ObjectState.Modified:
        //            _items.Remove(entity);
        //            _items.Add(entity);
        //            break;

        //        case ObjectState.Deleted:
        //            _items.Remove(entity);
        //            break;

        //        case ObjectState.Unchanged:
        //        case ObjectState.Added:
        //            _items.Add(entity);
        //            break;

        //        default:
        //            throw new ArgumentOutOfRangeException();
        //    }
        //    return entity;
        //}

        //public override TEntity Create() { return new TEntity(); }

        //public override TDerivedEntity Create<TDerivedEntity>() { return Activator.CreateInstance<TDerivedEntity>(); }

        //public override ObservableCollection<TEntity> Local { get { return _items; } }
    }
}
