using FluentAssertions;
using Lcp.Application.Schema.Queries;
using Lcp.Tests.Fixtures;
using Lcp.Tests.Generators;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Lcp.Tests.Schema.Queries
{
    /// <summary>
    ///     Provides the unit tests for
    ///     the sample queries.
    /// </summary>
    public class SampleQueryTests : IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;

        /// <summary>
        ///     Initializes a new instance of the 
        ///     <see cref="SampleQueryTests"/> 
        ///     class.
        /// </summary>
        public SampleQueryTests(TestFixture testFixture) => _fixture = testFixture;

        [Fact]
        public async Task GetSamples_OnSuccess_ReturnsSamples()
        {
            //Arrange
            var query = new SampleQuery();
            var dbContext = _fixture.GetDbContext();
            var data = SampleGenerator.GetSamples(10);

            await dbContext.Samples.AddRangeAsync(data);
            await dbContext.SaveChangesAsync();

            //Act
            var result = query.GetSamples(dbContext);

            //Assert
            result.Should().BeEquivalentTo(
                data,
                opt => opt.ExcludingMissingMembers()
            );
        }

        [Fact]
        public async Task GetSampleById_OnSuccess_ReturnsSample()
        {
            //Arrange
            var query = new SampleQuery();
            var dbContext = _fixture.GetDbContext();
            var data = SampleGenerator.GetSamples(1).First();

            await dbContext.AddAsync(data);
            await dbContext.SaveChangesAsync();

            //Act
            var result = query.GetSampleById(
                dbContext,
                data.Id
            );

            //Assert
            result.First().Should().BeEquivalentTo(
                data,
                opt => opt.ExcludingMissingMembers()
            );
        }

        [Fact]
        public async Task GetSamplesByIds_OnSuccess_ReturnsSamples()
        {
            //Arrange
            var query = new SampleQuery();
            var dbContext = _fixture.GetDbContext();
            var data = SampleGenerator.GetSamples(10).ToList();

            await dbContext.Samples.AddRangeAsync(data);
            await dbContext.SaveChangesAsync();

            //Act
            var result = query.GetSamplesByIds(
                dbContext,
                data.Select(d => d.Id).ToArray()
            );

            //Assert
            result.Should().BeEquivalentTo(
                data,
                opt => opt.ExcludingMissingMembers()
            );
        }

        [Fact]
        public async Task GetSamplesByIds_WithOneBadId_ReturnsValidExistingData()
        {
            var query = new SampleQuery();
            var dbContext = _fixture.GetDbContext();
            var data = SampleGenerator.GetSamples(2)
                                                .ToList();

            await dbContext.Samples.AddRangeAsync(data);
            await dbContext.SaveChangesAsync();

            var result = query.GetSamplesByIds(
                dbContext,
                data.Take(2)
                    .Select(x => x.Id)
                    .Concat(
                        new[]
                        {
                            new Guid()
                        }
                    )
                    .ToArray()
            );

            result.Should()
                  .BeEquivalentTo(data);
        }
    }
}
