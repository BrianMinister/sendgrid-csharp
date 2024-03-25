using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System;
using System.Security.Principal;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;


namespace Lcp.Api.AuthenticationProviders
{
    /// <summary>
    ///     Provides an implementation of the <see cref="SqlAuthenticationProvider"/> 
    ///     that implements the AD Interactive SQL authentication.
    ///     <para>
    ///         References:<br />
    ///         <a href="https://www.pluralsight.com/guides/how-to-use-managed-identity-with-azure-sql-database" /><br />
    ///         <a href="https://github.com/Azure/azure-sdk-for-net/blob/master/sdk/mgmtcommon/AppAuthentication/Azure.Services.AppAuthentication/SqlAppAuthenticationProvider.cs" />
    ///     </para>
    /// </summary>
    public class SqlAppAuthenticationProvider : SqlAuthenticationProvider
    {
        /// <summary>
        ///      Gets or sets the principal used to acquire a 
        ///      token. This will be of type "User" for local 
        ///      development scenarios, and "App" when client 
        ///      credentials flow is used. 
        /// </summary>
        public IPrincipal? PrincipalUsed { get; private set; }

        /// <summary>
        ///     Gets a token provider connection string
        ///     for the given user ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The connection string if <paramref name="userId"/> is valid; otherwise, <c>null</c>.</returns>
        internal static string? GetConnectionStringByUserId(string userId)
        {
            if (Guid.TryParse(userId, out _))
            {
                return $"RunAs=App;AppId={userId}";
            }

            return default;
        }

        /// <summary>
        ///     Acquires a security token from 
        ///     the authority.
        /// </summary>
        /// <param name="parameters">The SQL authentication parameters.</param>
        /// <returns>The SQL authentication token.</returns>
        /// <exception cref="ArgumentNullException">The Azure AD Instance is <see langword="null" /></exception>
        /// <exception cref="ArgumentNullException">The resource is <see langword="null" /></exception>
        public override async Task<SqlAuthenticationToken> AcquireTokenAsync(SqlAuthenticationParameters parameters)
        {
            string? azureAdInstance = UriHelper.GetAzureAdInstanceByAuthority(parameters.Authority);
            string? connectionString = GetConnectionStringByUserId(parameters.UserId);
            string? tenantId = UriHelper.GetTenantByAuthority(parameters.Authority);

            if (string.IsNullOrEmpty(azureAdInstance))
                throw new ArgumentNullException("The Azure AD instance could not be parsed from the authority provided in SqlAuthenticationParameters.");

            if (string.IsNullOrEmpty(parameters.Resource))
                throw new ArgumentNullException("A resource must be specified in SqlAuthenticationParameters.");

            //var tokenProvider = new AzureServiceTokenProvider(connectionString, azureAdInstance);
            //var authResult = await tokenProvider.GetAuthenticationResultAsync(parameters.Resource, tenantId)
            //                                    .ConfigureAwait(false);

            //PrincipalUsed = tokenProvider.PrincipalUsed;

            //return new SqlAuthenticationToken(authResult.AccessToken, authResult.ExpiresOn);

            var credential = new DefaultAzureCredential();
            var tokenRequestContext = new TokenRequestContext(new[] { parameters.Resource });
            var tokenResponse = await credential.GetTokenAsync(tokenRequestContext);

            return new SqlAuthenticationToken(tokenResponse.Token, tokenResponse.ExpiresOn);

        }

        /// <summary>
        ///     Indicates whether the specified
        ///     authentication method is supported.
        /// </summary>
        /// <param name="authenticationMethod">The SQL authentication method.</param>
        /// <returns><c>true</c> if the authentication method is supported; otherwise, <c>false</c>.</returns>
        public override bool IsSupported(SqlAuthenticationMethod authenticationMethod) =>
            authenticationMethod == SqlAuthenticationMethod.ActiveDirectoryInteractive;
    }
}
