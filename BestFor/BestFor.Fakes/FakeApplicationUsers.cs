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
        public const int DEFAUL_NUMBER_USERS = 4;
        public const string DEFAUL_USER_ID = "0";

        public FakeApplicationUsers() : base()
        {
            _items = new ObservableCollection<ApplicationUser>();
            _query = _items.AsQueryable();
            Add(new ApplicationUser{ Id = "0" });
            Add(new ApplicationUser { Id = "1" });
            Add(new ApplicationUser { Id = "2" });
            Add(new ApplicationUser { Id = "3" });
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

        #region IQueryable implementation
        public Type ElementType { get { return _query.ElementType; } }

        public Expression Expression { get { return _query.Expression; } }

        public IQueryProvider Provider { get { return _query.Provider; } }
        #endregion

    }
}
