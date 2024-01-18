using Lcp.Api.Extensions;
using Lcp.Infrastructure.EF.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Lcp.Api
{
    /// <summary>
    ///     Provides startup configuration
    ///     for initializing services.
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     Gets the application configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///     Get the web hosting environment.
        /// </summary>
        public IWebHostEnvironment Environment { get; }

        /// <summary>
        ///     Initializes a new instance of the 
        ///     <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="env">The web host environment.</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        /// <summary>
        ///     Adds services to the container.
        ///     Method gets called by the runtime.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var applicationAssembly = Assembly.Load("Lcp.Application");

            if (!System.Diagnostics.Debugger.IsAttached)
                services.AddApplicationInsightsTelemetry();
            services.AddHttpContextAccessor();
            services.AddCorsConfiguration();
            services.AddDbContext(Configuration);
            services.AddFortify(Configuration);
            services.AddHotChocolate();
            services.AddAutoMapper(applicationAssembly);
            services.AddEvents(Configuration);
        }

        /// <summary>
        ///     Configures the HTTP request pipeline.
        ///     Method gets called by the runtime.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="environment">The web host environment.</param>
        /// <param name="autoMapper">The AutoMapper configuration provider.</param>
        /// <param name="dbContextFactory">The database context factory.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment,
            AutoMapper.IConfigurationProvider autoMapper, IDbContextFactory<TemplateContext> dbContextFactory)
        {
            if (environment.IsDevelopment() || environment.IsEnvironment("Local"))
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseWebSockets();
            app.UseRouting();
            app.UseCors("default");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

            autoMapper.AssertConfigurationIsValid();

            dbContextFactory.CreateDbContext().Database.Migrate();
        }
    }
}
