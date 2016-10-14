using BestFor.Domain;
using BestFor.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace BestFor.Fakes
{
    /// <summary>
    /// Base class for fake dbset implementation. Used in unit tests.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class FakeDbSet<TEntity> : DbSet<TEntity>, //IOrderedQueryable<TEntity>, 
        IEnumerable<TEntity> //, IEnumerable, IOrderedQueryable, 
        , IQueryable
        , IQueryable<TEntity>
        //, IAsyncEnumerableAccessor<TEntity>, IInfrastructure<IServiceProvider>
        where TEntity : class, IEntityBase, IObjectState, new()
    {
        #region Private Fields
        private readonly ObservableCollection<TEntity> _items;
        private readonly IQueryable _query;
        #endregion Private Fields

        protected FakeDbSet()
        {
            _items = new ObservableCollection<TEntity>();
            _query = _items.AsQueryable();
        }

   //      IEnumerator IEnumerable.GetEnumerator() { return _items.GetEnumerator(); }
        public IEnumerator<TEntity> GetEnumerator() { return _items.GetEnumerator(); }

        #region IQueryable implementation
        public Type ElementType { get { return _query.ElementType; } }

        public Expression Expression { get { return _query.Expression; } }

        public IQueryProvider Provider { get { return _query.Provider; } }
        #endregion

        public override EntityEntry<TEntity> Add(TEntity entity)
        {
            _items.Add(entity);
            return default(EntityEntry<TEntity>);
        }

        public override EntityEntry<TEntity> Update(TEntity entity)
        {
            return default(EntityEntry<TEntity>);
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

        #region DbSet<TEntity> virtuals implementation
        public override EntityEntry<TEntity> Attach(TEntity entity)
        {
            switch (entity.ObjectState)
            {
                case ObjectState.Modified:
                    _items.Remove(entity);
                    _items.Add(entity);
                    break;

                case ObjectState.Deleted:
                    _items.Remove(entity);
                    break;

                case ObjectState.Unchanged:
                case ObjectState.Added:
                    _items.Add(entity);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            // Fake fake fake!
            return default(EntityEntry<TEntity>);
            // TODO. Cloud not solve this.
            //return null;
        }
        #endregion

        //public override TEntity Create() { return new TEntity(); }

        //public override TDerivedEntity Create<TDerivedEntity>() { return Activator.CreateInstance<TDerivedEntity>(); }

        //public override ObservableCollection<TEntity> Local { get { return _items; } }
    }
}
