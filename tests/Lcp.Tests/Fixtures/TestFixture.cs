using AutoMapper;
using Bogus;
using Lcp.Infrastructure.EF.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;

namespace Lcp.Tests.Fixtures
{
    /// <summary>
    ///     Provides the test fixture for
    ///     the template unit tests.
    /// </summary>
    public class TestFixture : IDisposable
    {
        private readonly IServiceCollection _services;

        /// <summary>
        ///     Gets or sets the automapper
        ///     configuration.
        /// </summary>
        public IMapper Mapper { get; set; }

        /// <summary>
        ///     Gets or sets the http 
        ///     context accessor.
        /// </summary>
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        /// <summary>
        ///     Gets or sets the system
        ///     default user ID.
        /// </summary>
        public string DefaultUserId = "system@lcptracker.com";

        /// <summary>
        ///     Initializes a new instance of the 
        ///     <see cref="TestFixture"/> 
        ///     class.
        /// </summary>
        public TestFixture()
        {
            Trace.TraceInformation($"TestFixture::TestFixture()");

            _services = new ServiceCollection();
            _services.AddDbContextPool<TemplateContext>(
                op => op.UseInMemoryDatabase(
                    Guid.NewGuid()
                        .ToString()
                )
            );

            HttpContextAccessor = GetHttpContextAccessor().Object;
            Mapper = new MapperConfiguration(c => c.AddMaps("Lcp.Application")).CreateMapper();
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        /// <summary>
        ///     Gets an in-memory database instance 
        ///     of the database context.
        /// </summary>
        /// <returns>The database context.</returns>
        public TemplateContext GetDbContext()
        {
            Trace.TraceInformation($"TestFixture::GetContext()");

            var sp = _services.BuildServiceProvider();
            var dbContext = sp.GetService<TemplateContext>();

            dbContext.Database.EnsureCreated();

            return dbContext;
        }

        /// <summary>
        ///     Gets an in-memory database instance 
        ///     of the database context factory.
        /// </summary>
        /// <returns>The database context factory.</returns>
        public IDbContextFactory<TemplateContext> GetDbContextFactory()
        {
            Trace.TraceInformation($"TestFixture::GetDbContextFactory()");

            var mockDbContextFactory = new Mock<IDbContextFactory<TemplateContext>>();
            mockDbContextFactory.Setup(x => x.CreateDbContext())
                .Returns(GetDbContext());

            return mockDbContextFactory.Object;
        }

        /// <summary>
        ///     Gets a mock instance of the 
        ///     http context accessor.
        /// </summary>
        /// <returns>The mock http context accessor.</returns>
        private static Mock<IHttpContextAccessor> GetHttpContextAccessor()
        {
            Trace.TraceInformation($"TestFixture::GetHttpContextAccessor()");

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var httpContext = new DefaultHttpContext();
            var claims = new List<Claim>() {
                new Claim("user_name", new Faker().Person.Email)
            };

            httpContext.User.AddIdentity(new ClaimsIdentity(claims));

            mockHttpContextAccessor.SetupGet(s => s.HttpContext)
                .Returns(httpContext);

            return mockHttpContextAccessor;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Trace.TraceInformation($"TestFixture::Dispose()");
            using (var sp = _services.BuildServiceProvider())
            {
                var context = sp.GetService<TemplateContext>()!;
                context.Database.EnsureDeleted();
                context.Dispose();
            }

            _services.Clear();

            GC.SuppressFinalize(this);
        }
    }
}
