using BestFor.Domain.Entities;
using BestFor.Dto.Account;
using System.Collections.Generic;
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
        ApplicationUser FindByDisplayName(string displayName);

        ApplicationUser FindById(string id);

        /// <summary>
        /// Find users by a set of ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<ApplicationUserDto> FindByIds(List<string> ids);

        int AddUserToCache(ApplicationUser user);

        /// <summary>
        /// Increase the answer count in user's cache.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        int UpdateUserFromAnswer(Answer answer);

        /// <summary>
        /// List all users
        /// </summary>
        /// <returns></returns>
        IEnumerable<ApplicationUser> FindAll();

    }
}
