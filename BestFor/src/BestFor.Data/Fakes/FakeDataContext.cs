using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using BestFor.Domain.Entities;

namespace BestFor.Data.Fakes
{
    public class FakeDataContext : IDataContext
    {
        #region Private Fields
        /// <summary>
        /// Stores all fake db sets
        /// </summary>
        private readonly Dictionary<Type, object> _fakeDbSets;
        #endregion Private Fields

        public FakeDataContext()
        {
            _fakeDbSets = new Dictionary<Type, object>();
            AddFakeDbSet<Answer, FakeAnswers>();
        }

        public virtual DbSet<TEntity> EntitySet<TEntity>() where TEntity : class
        {
            return (DbSet<TEntity>)_fakeDbSets[typeof(TEntity)];
        }

        public void AddFakeDbSet<TEntity, TFakeDbSet>()
            where TEntity : EntityBase, new()
            where TFakeDbSet : FakeDbSet<TEntity>, new() // IDbSet<TEntity>, 
        {
            var fakeDbSet = Activator.CreateInstance<TFakeDbSet>();
            _fakeDbSets.Add(typeof(TEntity), fakeDbSet);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken) { return new Task<int>(() => default(int)); }

        public Task<int> SaveChangesAsync() { return new Task<int>(() => default(int)); }

        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState
        {
            // no implentation needed, unit tests which uses FakeDbContext since there is no actual database for unit tests, 
            // there is no actual DbContext to sync with, please look at the Integration Tests for test that will run against an actual database.
        }
    }
}
