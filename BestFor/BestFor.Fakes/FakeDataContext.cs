using BestFor.Data;
using BestFor.Domain.Interfaces;
using BestFor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace BestFor.Fakes
{
    public class FakeDataContext : IDataContext, IUserStore<ApplicationUser>, IQueryableUserStore<ApplicationUser>
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
            AddFakeDbSet<AnswerVote, FakeAnswerVotes>();
            AddFakeDbSet<AnswerDescriptionVote, FakeAnswerDescriptionVotes>();
            
            // Have to deal with users separately
            _fakeDbSets.Add(typeof(ApplicationUser), new FakeApplicationUsers());
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

        #region IUserStore<ApplicationUser> implementation
        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IUserStore<ApplicationUser> implementation
        public IQueryable<ApplicationUser> Users
        {
            get
            {
                var users = (FakeApplicationUsers)_fakeDbSets[typeof(ApplicationUser)];
                return users.Users;
            }
        }
        #endregion
    }
}
