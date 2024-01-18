using System;

namespace Lcp.Api.AuthenticationProviders
{
    /// <summary>
    ///     Provides helpers for parsing the Azure AD authority URL.
    ///     <para>
    ///         References:<br />
    ///         <a href="https://github.com/Azure/azure-sdk-for-net/blob/master/sdk/mgmtcommon/AppAuthentication/Azure.Services.AppAuthentication/Helpers/UriHelper.cs" />
    ///     </para>
    /// </summary>
    internal class UriHelper
    {
        /// <summary>
        ///     Gets the instance URL from the
        ///     given Azure AD authority URL. 
        ///     .
        /// </summary>
        /// <param name="authority">The Azure AD authority.</param>
        /// <returns>The instance URL if <paramref name="authority"/> is valid; otherwise, <c>null</c>.</returns>
        internal static string? GetAzureAdInstanceByAuthority(string authority)
        {
            if (!string.IsNullOrWhiteSpace(authority))
            {
                if (Uri.TryCreate(authority, UriKind.Absolute, out Uri? authorityUri))
                {
                    if (authorityUri.Scheme != "https")
                    {
                        return null;
                    }

                    string path = authorityUri.AbsolutePath.Substring(1);

                    if (string.IsNullOrWhiteSpace(path))
                    {
                        return null;
                    }

                    return $"https://{authorityUri.Host}/";
                }
            }

            return null;
        }

        /// <summary>
        ///     Gets the tenant for the given
        ///     Azure AD authority URL.
        /// </summary>
        /// <param name="authority">The Azure AD authority.</param>
        /// <returns>The tenant if <paramref name="authority"/> is valid; otherwise, <c>null</c>.</returns>
        internal static string? GetTenantByAuthority(string authority)
        {
            if (!string.IsNullOrWhiteSpace(authority))
            {
                if (Uri.TryCreate(authority, UriKind.Absolute, out Uri? authorityUri))
                {
                    if (authorityUri?.Segments.Length >= 2)
                    {
                        return authorityUri.Segments[1].TrimEnd('/');
                    }
                }
            }

            return null;
        }
    }
}
