using Bogus;
using FluentAssertions;
using Lcp.Application.InputModels;
using Lcp.Application.Schema.Mutations;
using Lcp.Domain.Entities;
using Lcp.Microsvs.Events;
using Lcp.Tests.Fixtures;
using Lcp.Tests.Generators;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace Lcp.Tests.Schema.Mutations
{
    /// <summary>
    ///     Provides the unit tests for
    ///     the sample mutations.
    /// </summary>
    public class SampleMutationTests : IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="SampleMutationTests"/>
        ///     class.
        /// </summary>

        public SampleMutationTests(TestFixture testFixture) => _fixture = testFixture;

        [Fact]
        public async Task CreateSample_OnSuccess_ReturnsNewSample()
        {
            //Arrange
            var mutation = new SampleMutation();
            var mockSender = new Mock<IEventSender>();
            var dbContext = _fixture.GetDbContext();
            var input = new SampleInputModel
            {
                Name = new Faker().Address.County()
            };

            //Act
            var result = await mutation.CreateSample(
                                    dbContext,
                                    _fixture.HttpContextAccessor,
                                    _fixture.Mapper,
                                    mockSender.Object,
                                    input
                                );

            //Assert
            var entity = await dbContext.Samples.FindAsync(result.Data.Id);

            result.Data.Should().BeEquivalentTo(entity);
            result.Data.Id.Should().NotBeEmpty();
            result.Data.CreatedBy.Should().NotBe(_fixture.DefaultUserId);
            result.Data.CreatedBy.Should().Be(result.Data.ModifiedBy);
            result.Data.CreatedDate.Should().Be(result.Data.ModifiedDate);
            input.Should().BeEquivalentTo(
                entity,
                opt => opt.Excluding(x => x.Id)
                          .ExcludingMissingMembers()
            );
            mockSender.Verify(x => x.SendMessage("CreateSample", It.IsAny<EventMessageBase>()), Times.Once());
        }

        [Fact]
        public async Task CreateSample_WithNoHttpContext_UseDefaultUserId()
        {
            //Arrange
            var mutation = new SampleMutation();
            var dbContext = _fixture.GetDbContext();
            var mockSender = new Mock<IEventSender>();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var input = new SampleInputModel
            {
                Name = new Faker().Address.County()
            };

            //Act
            var result = await mutation.CreateSample(
                                    dbContext,
                                    mockHttpContextAccessor.Object,
                                    _fixture.Mapper,
                                    mockSender.Object,
                                    input
                                );

            //Assert
            result.Data.CreatedBy.Should().Be(_fixture.DefaultUserId);
        }

        [Fact]
        public async Task CreateSample_WithValidationErrors_ReturnsErrors()
        {
            //Arrange
            var mutation = new SampleMutation();
            var dbContext = _fixture.GetDbContext();
            var mockSender = new Mock<IEventSender>();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var input = new SampleInputModel();

            //Act
            var result = await mutation.CreateSample(
                                    dbContext,
                                    mockHttpContextAccessor.Object,
                                    _fixture.Mapper,
                                    mockSender.Object,
                                    input
                                );

            //Assert
            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task UpdateSample_OnSuccess_ReturnsUpdatedSample()
        {
            //Arrange
            var mutation = new SampleMutation();
            var dbContext = _fixture.GetDbContext();
            var mockSender = new Mock<IEventSender>();
            var data = SampleGenerator.GetSamples(1).First();
            var input = new SampleInputModel
            {
                Name = new Faker().Address.County()
            };

            await dbContext.AddAsync(data);
            await dbContext.SaveChangesAsync<Sample>(new DefaultHttpContext(), mockSender.Object);

            //Act
            var result = await mutation.UpdateSample(
                dbContext,
                _fixture.HttpContextAccessor,
                _fixture.Mapper,
                mockSender.Object,
                data.Id,
                input
            );

            //Assert
            var entity = await dbContext.Samples.FindAsync(result.Data.Id);
            var userName = _fixture.HttpContextAccessor.HttpContext.User
                               .FindFirst("user_name").Value;

            result.Data.Should().BeEquivalentTo(entity);
            result.Data.CreatedBy.Should().Be(_fixture.DefaultUserId);
            result.Data.ModifiedBy.Should().Be(userName);
            result.Data.CreatedBy.Should().NotBe(result.Data.ModifiedBy);
            result.Data.CreatedDate.Should().NotBe(result.Data.ModifiedDate);
            input.Should().BeEquivalentTo(
                entity,
                opt => opt.ExcludingMissingMembers()
            );
            mockSender.Verify(x => x.SendMessage("UpdateSample", It.IsAny<EventMessageBase>()), Times.Once());
        }

        [Fact]
        public async Task UpdateSample_WithInvalidId_ReturnsErrors()
        {
            //Arrange
            var mutation = new SampleMutation();
            var dbContext = _fixture.GetDbContext();
            var mockSender = new Mock<IEventSender>();
            var id = Guid.NewGuid();
            var input = new SampleInputModel
            {
                Name = new Faker().Address.County()
            };

            //Act
            var result = await mutation.UpdateSample(
                 dbContext,
                _fixture.HttpContextAccessor,
                _fixture.Mapper,
                mockSender.Object,
                id,
                input);

            //Assert
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public async Task UpdateSample_WithValidationErrors_ReturnsErrors()
        {
            //Arrange
            var mutation = new SampleMutation();
            var dbContext = _fixture.GetDbContext();
            var mockSender = new Mock<IEventSender>();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var input = new SampleInputModel();

            var data = SampleGenerator.GetSamples(1).First();

            await dbContext.AddAsync(data);
            await dbContext.SaveChangesAsync();

            //Act
            var result = await mutation.UpdateSample(
                                    dbContext,
                                    mockHttpContextAccessor.Object,
                                    _fixture.Mapper,
                                    mockSender.Object,
                                    data.Id,
                                    input
                                );
            //Assert
            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task DeleteSample_OnSuccess_ReturnsDeleteSuccess()
        {
            //Arrange
            var mutation = new SampleMutation();
            var dbContext = _fixture.GetDbContext();
            var data = SampleGenerator.GetSamples(1).First();
            var mockSender = new Mock<IEventSender>();

            await dbContext.AddAsync(data);
            await dbContext.SaveChangesAsync();

            //Act
            var result = await mutation.DeleteSample(
                dbContext,
                _fixture.HttpContextAccessor,
                mockSender.Object,
                data.Id
            );

            //Assert
            var entity = await dbContext.Samples.FindAsync(data.Id);

            result.Data.Deleted.Should().BeTrue();
            entity.IsDeleted.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteSample_WithInvalidId_ReturnsErrors()
        {
            //Arrange
            var mutation = new SampleMutation();
            var dbContext = _fixture.GetDbContext();
            var mockSender = new Mock<IEventSender>();
            var id = Guid.NewGuid();

            //Act
            var result = await mutation.DeleteSample(
                            dbContext,
                            _fixture.HttpContextAccessor,
                            mockSender.Object,
                            id);

            //Assert
            result.Errors.Count().Should().Be(1);
        }
    }
}
