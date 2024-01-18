using FluentAssertions;
using Lcp.Infrastructure.Events.EventHandlers;
using Lcp.Microsvs.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Xunit;

namespace Lcp.Tests.EventHandlers
{
    /// <summary>
    ///     Provides the unit tests for the
    ///     mutation event handler.
    /// </summary>
    public class SampleHandlerTests
    {
        [Theory]
        [InlineData("{ \"type\":\"testtype\", \"data\":\"testdata\" }", true)]
        [InlineData("", false)]
        public async Task HandleAsync_WithMessage_ReturnExpectedValue(string json, bool expected)
        {
            //Arrange
            var services = new ServiceCollection();
            services.AddLogging();

            await using var sp = services.BuildServiceProvider();

            var handler = new SampleHandler(
                sp.GetRequiredService<ILogger<SampleHandler>>()
            );

            //Act
            var result = await handler.HandleAsync("Test", json);

            //Assert
            result.Should().Be(expected);
        }
    }
}
