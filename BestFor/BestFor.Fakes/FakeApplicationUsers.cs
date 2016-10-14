using BestFor.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BestFor.Fakes
{
    /// <summary>
    /// Implements a fake dbset of known users. Used in unit tests.
    /// </summary>
    public class FakeApplicationUsers : DbSet<ApplicationUser>, IQueryable, IQueryable<ApplicationUser>//,
        //IQueryableUserStore<ApplicationUser>
    {
        #region Private Fields
        private readonly ObservableCollection<ApplicationUser> _items;
        private readonly IQueryable _query;
        #endregion Private Fields

        /// <summary>
        /// Make sure it is the same number as loaded in constructor.
        /// </summary>
        public const int DEFAUL_NUMBER_USERS = 1;

        public FakeApplicationUsers() : base()
        {
            _items = new ObservableCollection<ApplicationUser>();
            _query = _items.AsQueryable();
            Add(new ApplicationUser{ Id = "0" });
        }

        public IQueryable<ApplicationUser> Users
        {
            get
            {
                return _items.AsQueryable();
            }
        }

        public override EntityEntry<ApplicationUser> Add(ApplicationUser entity)
        {
            _items.Add(entity);
            return default(EntityEntry<ApplicationUser>);
        }

        public override EntityEntry<ApplicationUser> Update(ApplicationUser entity)
        {
            return default(EntityEntry<ApplicationUser>);
        }

        //#region IQueryableUserStore implementation
        //public IQueryable<ApplicationUser> Users { get { return _items.AsQueryable<ApplicationUser>(); } }


        //public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}
        //#endregion

        #region IQueryable implementation
        public Type ElementType { get { return _query.ElementType; } }

        public Expression Expression { get { return _query.Expression; } }

        public IQueryProvider Provider { get { return _query.Provider; } }
        #endregion

    }
}
