﻿using BestFor.Data;
using BestFor.Domain.Interfaces;
using BestFor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BestFor.Fakes
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
            AddFakeDbSet<BadWord, FakeBadWords>();
            AddFakeDbSet<Suggestion, FakeSuggestions>();
            AddFakeDbSet<AnswerDescription, FakeAnswerDescriptions>();
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

        /// <summary>
        /// Has to be virtual otherwise can not mock it using Moq
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken) { return Task.FromResult(0); }

        /// <summary>
        /// Has to be virtual otherwise can not mock it using Moq
        /// </summary>
        /// <returns></returns>
        public virtual Task<int> SaveChangesAsync() { return Task.FromResult(0); }

        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState
        {
            // no implentation needed, unit tests which uses FakeDbContext since there is no actual database for unit tests, 
            // there is no actual DbContext to sync with, please look at the Integration Tests for test that will run against an actual database.
        }
    }
}
