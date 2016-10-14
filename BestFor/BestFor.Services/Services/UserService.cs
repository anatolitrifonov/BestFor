using BestFor.Domain.Entities;
using BestFor.Dto.Account;
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
        public ApplicationUser FindByDisplayName(string displayName)
        {
            // Do not check nulls
            if (string.IsNullOrEmpty(displayName)) return null;
            if (string.IsNullOrWhiteSpace(displayName)) return null;

            // var// var

            // Find first user with this display name. 
            return _userManager.Users.FirstOrDefault(x => x.DisplayName == displayName);
        }

        /// <summary>
        /// This one is not 100% solid. If user is not in cache, null will be returned.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApplicationUser FindById(string id)
        {
            // Do not check nulls
            if (string.IsNullOrEmpty(id)) return null;
            if (string.IsNullOrWhiteSpace(id)) return null;

            // Get cache ... might take long but is not null
            var data = GetCachedData();

            ApplicationUser user;
            if (data.TryGetValue(id, out user))
                return user;

            return null;
        }

        /// <summary>
        /// Find users by a set of ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<ApplicationUserDto> FindByIds(List<string> ids)
        {
            // Do check nulls
            if (ids == null) return null;

            // Get cache ... might take long but is not null
            var data = GetCachedData();

            var result = new List<ApplicationUserDto>();

            // Build a list of users from ids.
            ApplicationUser user;
            foreach (var id in ids)
            {
                if (data.TryGetValue(id, out user))
                {
                    result.Add(user.ToDto());
                }
            }
            return result;
        }


        /// <summary>
        /// Cache user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int AddUserToCache(ApplicationUser user)
        {
            // load cache
            Dictionary<string, ApplicationUser> data = GetCachedData();
            // Something went wrong if this is null.
            if (data == null) return 0;

            if (!data.ContainsKey(user.Id))
                data.Add(user.Id, user);

            return 1;
        }

        /// <summary>
        /// Update user from answer.
        /// 
        /// Increase countes.
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public int UpdateUserFromAnswer(Answer answer)
        {
            if (answer.UserId == null) return 0;

            // load cache
            Dictionary<string, ApplicationUser> data = GetCachedData();
            // Something went wrong if this is null.
            if (data == null) return 0;

            ApplicationUser user;
            if (!data.TryGetValue(answer.UserId, out user)) return 0;

            // If user is in cache -> increase the count of added answers
            // This is cool but we are not going to rely on this number. See the load below.
            user.NumberOfAnswers++;

            return 1;
        }

        /// <summary>
        /// Find all users
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApplicationUser> FindAll()
        {
            var data = GetCachedData();

            var result = data.Values.AsEnumerable<ApplicationUser>();

            return result;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>The knows problem with this in that when my content is rendered
        /// this function is called for each answer description. Need to optimize somehow.</remarks>
        private Dictionary<string, ApplicationUser> GetCachedData()
        {
            object data = _cacheManager.Get(CacheConstants.CACHE_KEY_USERS_DATA);
            if (data == null)
            {
                var dataSource = new Dictionary<string, ApplicationUser>();

                // We are not going to load the number of answers per user.
                // We will let answer service to deal with this.
                // We will store index of all user answers there. Not here.
                // We  can theoretically do this hear too but let all the answers cache be deal with by answer service.
                foreach (var user in _userManager.Users)
                    dataSource.Add(user.Id, user);

                _cacheManager.Add(CacheConstants.CACHE_KEY_USERS_DATA, dataSource);
                return dataSource;
            }
            return (Dictionary<string, ApplicationUser>)data;
        }
        #endregion
    }
}
