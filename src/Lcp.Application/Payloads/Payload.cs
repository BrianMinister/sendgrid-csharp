using Lcp.Application.ErrorHandlers.Models;
using System.Collections.Generic;

namespace Lcp.Application.Payloads
{
    /// <summary>
    ///     Represents a generic payload to be
    ///     returned by GraphQL in a mutation.
    /// </summary>
    /// <typeparam name="T">The type of payload data being returned.</typeparam>
    public class Payload<T>
    {
        /// <summary>
        ///     Initializes a new instance of the 
        ///     <see cref="Payload{T}"/> 
        ///     class.
        /// </summary>
        /// <param name="data">The payload data.</param>
        public Payload(T data) => Data = data;

        /// <summary>
        ///     Initializes a new instance of the 
        ///     <see cref="Payload{T}"/> 
        ///     class.
        /// </summary>
        /// <param name="errors">The validation errors.</param>
        public Payload(IEnumerable<Error> errors) => Errors = errors;

        /// <summary>
        ///     Gets or sets the payload
        ///     data.
        /// </summary>
        public T? Data { get; set; }
        /// <summary>
        ///     Gets or sets the payload erros.
        /// </summary>
        public IEnumerable<Error>? Errors { get; set; }
    }
}
