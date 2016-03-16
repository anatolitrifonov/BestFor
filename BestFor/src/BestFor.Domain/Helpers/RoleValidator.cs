using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BestFor.Domain.Entities;

namespace BestFor.Domain.Helpers
{
    public class RoleValidator : IRoleValidator<IdentityRole>
    {
        /// <summary>
        /// I have no idea what this interface should do. Comments suck.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task<IdentityResult> ValidateAsync(RoleManager<IdentityRole> manager, IdentityRole role)
        {
            IdentityResult result = new IdentityResult();
            return Task.FromResult<IdentityResult>(result);
        }
    }
}
