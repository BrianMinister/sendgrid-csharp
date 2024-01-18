using Lcp.Microsvs.Events;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Lcp.Infrastructure.Events.EventHandlers
{
    /// <summary>
    ///     Provides event handling for samples.
    ///     TODO: Currently, this is bare-bones and does nothing.
    /// </summary>
    public class SampleHandler : IEventHandler
    {
        private readonly ILogger<SampleHandler> _logger;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="SampleHandler"/>
        ///     class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public SampleHandler(ILogger<SampleHandler> logger) => _logger = logger;

        /// <summary>
        ///     Handles the event message and returns
        ///     the success indicator.
        /// </summary>
        /// <param name="subject">The subject of the message.</param>
        /// <param name="data">The data being handled.</param>
        /// <returns><c>True</c> if the message was handled successfully, <c>false</c> otherwise.</returns>
        public Task<bool> HandleAsync(string subject, string data)
        {
            if (string.IsNullOrEmpty(data))
                return Task.FromResult(false);
            _logger.LogInformation("Received message {0}", data);
            return Task.FromResult(true);
        }
    }
}
