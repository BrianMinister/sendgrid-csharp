using Lcp.Api.AuthenticationProviders;
using Lcp.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetEscapades.AspNetCore.StartupTasks;
using System.Threading.Tasks;

namespace Lcp.Api
{
    /// <summary>
    ///     Provides the program entry 
    ///     class for the .Net Core program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        ///     Provides the program entry point.
        /// </summary>
        /// <param name="args">The collection of arguments.</param>
        public static async Task Main(string[] args)
        {
            SqlAuthenticationProvider.SetProvider(
                SqlAuthenticationMethod.ActiveDirectoryInteractive,
                new SqlAppAuthenticationProvider()
            );

            var webHost = CreateHostBuilder(args).Build();
            var startupTasks = webHost.Services.GetServices<IStartupTask>();

            foreach (var startupTask in startupTasks)
            {
                await startupTask.StartAsync();
            }

            await webHost.RunAsync();
        }

        /// <summary>
        ///      Starts the ASP.NET application using
        ///      a standard construction pattern.
        /// </summary>
        /// <param name="args">The collection of arguments.</param>
        /// <returns>The host builder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }
                ).AddKeyVaultConfig();
    }
}
