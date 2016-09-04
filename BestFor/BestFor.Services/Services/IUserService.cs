using BestFor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Services.Services
{
    /// <summary>
    /// Interface for user service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// In our world display name is supposed to be unique. This method helps finding user by display name.
        /// </summary>
        /// <param name="displayName"></param>
        /// <returns></returns>
        Task<ApplicationUser> FindByDisplayNameAsync(string displayName);

        Task<ApplicationUser> FindByIdAsync(string id);

        Task<int> AddUserToCache(ApplicationUser user);

        /// <summary>
        /// Increase the answer count in user's cache.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<int> UpdateUserFromAnswer(Answer answer);

        /// <summary>
        /// List all users
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ApplicationUser>> FindAll();

    }
}
