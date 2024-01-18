using Bogus;
using Lcp.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Lcp.Tests.Generators
{
    /// <summary>
    ///     Provides a generator for
    ///     fake samples.
    /// </summary>
    public static class SampleGenerator
    {
        /// <summary>
        ///     Gets a collection of fake 
        ///     sample entities.
        /// </summary>
        /// <param name="count">The number of samples requested.</param>
        /// <returns>The collection of samples.</returns>
        public static IEnumerable<Sample> GetSamples(int count)
        {
            var orgId = new Faker().Random.Guid();

            var faker = new Faker<Sample>().Rules(
                (f, o) =>
                {
                    o.Id = f.Random.Guid();
                    o.Name = f.Random.Word();
                }
            );

            return faker.Generate(count)
                        .AsEnumerable();
        }
    }
}
