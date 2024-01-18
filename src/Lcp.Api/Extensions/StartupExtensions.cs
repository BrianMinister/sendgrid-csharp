using Azure.Identity;
using Lcp.Api.Authorization;
using Lcp.Api.Models;
using Lcp.Infrastructure.EF.Contexts;
using Lcp.Infrastructure.Events.EventHandlers;
using Lcp.Microsvs.Events;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Azure.KeyVault;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetEscapades.AspNetCore.StartupTasks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lcp.Api.Extensions
{
    /// <summary>
    ///     Provides extension methods for
    ///     performing startup operations.
    /// </summary>
    public static class StartupExtensions
    {
        /// <summary>
        ///     Adds the configuration for the database
        ///     context to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The application configuration.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddPooledDbContextFactory<TemplateContext>(
                optionsBuilder =>
                {
                    optionsBuilder.UseLazyLoadingProxies()
                                  .UseSqlServer(
                                      configuration.GetConnectionString("ConnectionString"),
                                      s =>
                                      {
                                          s.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                                          s.MigrationsHistoryTable("__MigrationHistory");
                                      }
                                  )
                                  .EnableDetailedErrors()
                                  .EnableSensitiveDataLogging();
                }
            );

        /// <summary>
        ///     Adds the configuration for CORS
        ///     to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services) =>
            services.AddCors(
                options =>
                {
                    // this defines a CORS policy called "default"
                    options.AddPolicy(
                        "default",
                        policy =>
                        {
                            policy.AllowAnyHeader()
                                  .AllowAnyMethod()
                                  .AllowAnyOrigin();
                        });
                });

        /// <summary>
        ///     Adds the Fortify authentication and authorization 
        ///     options to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The application configuration.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddFortify(this IServiceCollection services, IConfiguration configuration)
        {

            var fortifyOptions = new FortifyOptions();

            services.AddTransient<IAuthorizationHandler, FortifyPermissionHandler>();

            configuration.GetSection("Fortify")
                         .Bind(fortifyOptions);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(
                        JwtBearerDefaults.AuthenticationScheme,
                        cfg =>
                        {
                            cfg.SaveToken = true;
                            cfg.Authority = fortifyOptions.Authority;
                            cfg.Audience = fortifyOptions.ApiScope;
                            cfg.RequireHttpsMetadata = true;
                        }
                    );

            services.AddAuthorization(
                options =>
                {
                    options.AddPolicy("Fortify", policy => policy.Requirements.Add(new FortifyRequirement()));
                }
            );

            return services;
        }

        /// <summary>
        ///     Adds the event sourcing receiver and sender objects
        ///     to the service collection and registers the event
        ///     handlers.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The application configuration.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddEvents(this IServiceCollection services, IConfiguration configuration)
        {
            var eventsOptions = new EventsOptions();
            configuration.GetSection("Events")
                         .Bind(eventsOptions);

            var actualConnectionString = configuration.GetValue<string>(eventsOptions.ConnectionString);
            eventsOptions.ConnectionString = actualConnectionString;

            services.AddSingleton(
                sp => new EventWorkItemQueue(sp.GetRequiredService<ILogger<EventWorkItemQueue>>())
            );

            services.AddHostedService(
                sp => new SendMessageService(
                    sp.GetRequiredService<ILogger<SendMessageService>>(),
                    eventsOptions.ConnectionString,
                    eventsOptions.TopicName,
                    eventsOptions.DelayTime,
                    sp.GetRequiredService<EventWorkItemQueue>()
                )
            );

            services.AddTransient<SampleHandler>();

            services.AddSingleton(
                sp =>
                {
                    var er = new Receiver(
                        sp.GetRequiredService<ILogger<Receiver>>(),
                        sp,
                        eventsOptions
                    );
                    // call register handler here
                    er.RegisterHandler("Sample", typeof(SampleHandler));
                    return er;
                }
            );

            services.AddTransient<IEventSender>(
                sp => new Sender(
                    sp.GetRequiredService<ILogger<Sender>>(),
                    sp.GetRequiredService<EventWorkItemQueue>()
                )
            );

            services.AddTransient<IStartupTask, StartEventReceiver>();

            return services;
        }

        /// <summary>
        ///      Provides methods for running async 
        ///      tasks on application startup.
        ///      <para>
        ///         References:<br />
        ///         <a href="https://andrewlock.net/running-async-tasks-on-app-startup-in-asp-net-core-part-2/"/>
        ///      </para>
        /// </summary>
        private class StartEventReceiver : IStartupTask
        {
            private readonly IServiceProvider _serviceProvider;

            /// <summary>
            ///     Initializes a new instance of the 
            ///     <see cref="StartEventReceiver"/> 
            ///     class.
            /// </summary>
            /// <param name="serviceProvider">The service provider.</param>
            public StartEventReceiver(IServiceProvider serviceProvider) =>
                _serviceProvider = serviceProvider;

            /// <summary>
            ///   Starts an asynchronous operation.
            /// </summary>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>An asynchronous operation.</returns>
            public async Task StartAsync(CancellationToken cancellationToken = new())
            {
                using var scope = _serviceProvider.CreateScope();
                var receiver = scope.ServiceProvider.GetRequiredService<Receiver>();
                await receiver.Initialize(cancellationToken);
            }

            /// <summary>
            ///     Shuts down an asynchronous operation.
            /// </summary>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>An asynchronous operation.</returns>
            public async Task ShutdownAsync(CancellationToken cancellationToken = new())
            {
                using var scope = _serviceProvider.CreateScope();
                var receiver = scope.ServiceProvider.GetRequiredService<Receiver>();

                await receiver.DisposeAsync();
            }
        }

        /// <summary>
        ///     Set up KeyVault configuration 
        ///     fetching for the application.
        /// </summary>
        /// <param name="builder">The host builder.</param>
        /// <returns>The host builder.</returns>
        public static IHostBuilder AddKeyVaultConfig(this IHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                var builtConfig = config.Build();
                var vaultName = builtConfig["VaultName"];
                var keyVaultClient = new KeyVaultClient(
                    (authority, resource, scope) =>
                    {
                        var credential = new DefaultAzureCredential(false);
                        var token = credential.GetToken(
                            new Azure.Core.TokenRequestContext(
                                new[] { "https://vault.azure.net/.default" }));

                        return Task.FromResult(token.Token);
                    });

                config.AddAzureKeyVault(
                    vaultName,
                    keyVaultClient,
                    new DefaultKeyVaultSecretManager());
            });

            return builder;
        }
    }
}
