using Lcp.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace Lcp.Api.Authorization
{
    /// <summary>
    ///     Provides a permission handler for  
    ///     checking against the Fortify RBAC.
    ///     TODO: Currently this only checks for 
    ///     email verification as PoC.
    /// </summary>
    public class FortifyPermissionHandler : AuthorizationHandler<FortifyRequirement>
    {
        /// <summary>
        ///     Gets a value indicating whether the 
        ///     user's claim contains 'email_verified'.
        /// </summary>
        /// <param name="context">The authorization handler context.</param>
        /// <param name="requirement">The fortify requirement.</param>
        /// <returns>An asynchronous operation.</returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, FortifyRequirement requirement)
        {
            if (context.User.FindFirst(
                x => x.Type == "email_verified"
                     && x.Value.Equals("true", StringComparison.CurrentCultureIgnoreCase)) != null)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}