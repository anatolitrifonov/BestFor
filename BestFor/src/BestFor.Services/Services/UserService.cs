using BestFor.Data;
using BestFor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BestFor.Services.Services
{
    /// <summary>
    /// Implements additional operations with users. This service helps.
    /// </summary>
    public class UserService : IUserService
    {
        // Our path to users.
        private IdentityDbContext<ApplicationUser> _userContext;
        private ILogger _logger;

        public UserService(IDataContext context, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<VoteService>();
            _logger.LogInformation("created UserService");
            // This is a pure hack of course.
            _userContext = context as IdentityDbContext<ApplicationUser>;
        }

        public Task<ApplicationUser> FindByDisplayNameAsync(string displayName)
        {
            // Do not check nulls
            if (string.IsNullOrEmpty(displayName)) return Task.FromResult<ApplicationUser>(null);
            if (string.IsNullOrWhiteSpace(displayName)) return Task.FromResult<ApplicationUser>(null);

            // Find first user with this display name. 
            return Task.FromResult<ApplicationUser>(_userContext.Users.FirstOrDefault(x => x.DisplayName == displayName));
        }
    }
}
