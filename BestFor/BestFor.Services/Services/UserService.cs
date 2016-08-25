using BestFor.Domain.Entities;
using BestFor.Services.Cache;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Services.Services
{
    /// <summary>
    /// Implements additional operations with users. This service helps.
    /// </summary>
    public class UserService : IUserService
    {
        // Our path to users.
        //private IdentityDbContext<ApplicationUser> _userContext;
        private ILogger _logger;
        private UserManager<ApplicationUser> _userManager;
        private ICacheManager _cacheManager;

        public UserService(ICacheManager cacheManager, UserManager<ApplicationUser> userManager, ILoggerFactory loggerFactory)
        {
            _cacheManager = cacheManager;
            _logger = loggerFactory.CreateLogger<VoteService>();
            _logger.LogInformation("created UserService");
            // This is a pure hack of course.
            //_userContext = context as IdentityDbContext<ApplicationUser>;
            _userManager = userManager;
        }

        #region IUserService implementation
        public Task<ApplicationUser> FindByDisplayNameAsync(string displayName)
        {
            // Do not check nulls
            if (string.IsNullOrEmpty(displayName)) return Task.FromResult<ApplicationUser>(null);
            if (string.IsNullOrWhiteSpace(displayName)) return Task.FromResult<ApplicationUser>(null);

            // var// var

            // Find first user with this display name. 
            return Task.FromResult(_userManager.Users.FirstOrDefault(x => x.DisplayName == displayName));
        }

        /// <summary>
        /// This one is not 100% solid. If user is not in cache, null will be returned.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ApplicationUser> FindByIdAsync(string id)
        {
            // Do not check nulls
            if (string.IsNullOrEmpty(id)) return Task.FromResult<ApplicationUser>(null);
            if (string.IsNullOrWhiteSpace(id)) return Task.FromResult<ApplicationUser>(null);

            // Get cache ... might take long but is not null
            var data = GetCachedData();

            ApplicationUser user;
            if (data.TryGetValue(id, out user))
                return Task.FromResult(user);

            return Task.FromResult<ApplicationUser>(null);

            // Find first user with this display name. 
            // return Task.FromResult(_userManager.Users.FirstOrDefault(x => x.DisplayName == displayName));
        }

        public Task<int> AddUserToCache(ApplicationUser user)
        {
            // load cache
            Dictionary<string, ApplicationUser> data = GetCachedData();
            // Something went wrong if this is null.
            if (data == null) return Task.FromResult(0);

            if (!data.ContainsKey(user.Id))
                data.Add(user.Id, user);

            return Task.FromResult(1);
        }

        public Task<int> UpdateUserFromAnswer(Answer answer)
        {
            var badResult = Task.FromResult(0);
            if (answer.UserId == null) return badResult;

            // load cache
            Dictionary<string, ApplicationUser> data = GetCachedData();
            // Something went wrong if this is null.
            if (data == null) return badResult;

            ApplicationUser user;
            if (!data.TryGetValue(answer.UserId, out user)) return badResult;

            // If user is in cache -> increase the count of added answers
            user.NumberOfAnswers++;

            return Task.FromResult(1);
        }

        #endregion

        #region Private Methods
        private Dictionary<string, ApplicationUser> GetCachedData()
        {
            object data = _cacheManager.Get(CacheConstants.CACHE_KEY_USERS_DATA);
            if (data == null)
            {
                var dataSource = new Dictionary<string, ApplicationUser>();
                foreach(var user in _userManager.Users)
                    dataSource.Add(user.Id, user);
                _cacheManager.Add(CacheConstants.CACHE_KEY_USERS_DATA, dataSource);
                return dataSource;
            }
            return (Dictionary<string, ApplicationUser>)data;
        }
        #endregion
    }
}
