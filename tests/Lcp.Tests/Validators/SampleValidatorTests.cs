using Bogus;
using FluentValidation.TestHelper;
using Lcp.Application.InputModels;
using Lcp.Application.Validators;
using Lcp.Infrastructure.EF.Contexts;
using Lcp.Tests.Fixtures;
using Lcp.Tests.Generators;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Lcp.Tests.Validators
{
    /// <summary>
    ///     Provides unit tests for the
    ///     sample validator.
    /// </summary>
    public class SampleValidatorTests : IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;
        private readonly SampleValidator _validator;
        private readonly Faker _faker;
        private readonly TemplateContext _dbContext;

        /// <summary>
        ///     Initializes a new instance of the 
        ///     <see cref="SampleValidatorTests"/>  
        ///     class.
        /// </summary>
        public SampleValidatorTests(TestFixture testFixture)
        {
            _fixture = testFixture;
            _dbContext = _fixture.GetDbContext();
            _validator = new SampleValidator(_dbContext);
            _faker = new Faker("en");
        }

        [Fact]
        public void ValidateSampleName_WithValue_ShouldNotHaveErrors()
        {
            var result = _validator.TestValidate(new SampleInputModel { Name = _faker.Address.County() });
            result.IsValid.Should()
                  .BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ValidateSampleName_WithNoValue_ShouldHaveErrors(string value)
        {
            var result = _validator.TestValidate(new SampleInputModel{ Name = value});
            result.IsValid.Should()
                  .BeFalse();
        }
        [Theory]
        [InlineData(49)]
        [InlineData(50)]
        public void ValidateSampleName_WithValidLength_ShouldNotHaveErrors(int value)
        {
            var result = _validator.TestValidate(
                new SampleInputModel
                {
                    Name = string.Concat(Enumerable.Repeat("A", value))
                }
            );
            result.IsValid.Should()
                  .BeTrue();
        }
        [Fact]
        public void ValidateSampleName_WithInvalidLength_ShouldHaveErrors()
        {
            var result = _validator.TestValidate(
                new SampleInputModel { Name = string.Concat(Enumerable.Repeat("A", 51)) }
            );
            result.IsValid.Should()
                  .BeFalse();
        }
        [Fact]
        public async Task ValidateSampleName_WithDuplicateName_ShouldHaveErrors()
        {
            var data = SampleGenerator.GetSamples(1).First();

            await _dbContext.Samples.AddAsync(data);
            await _dbContext.SaveChangesAsync();

            var result = _validator.TestValidate(
                new SampleInputModel
                {
                    Name = data.Name
                });
            result.IsValid.Should()
                  .BeFalse();
        }

        [Fact]
        public async Task ValidateSample_WithDuplicateIdAndName_ShouldNotHaveErrors()
        {
            var data = SampleGenerator.GetSamples(1).First();
            var validator = new SampleValidator(_dbContext, data.Id);

            await _dbContext.AddAsync(data);
            await _dbContext.SaveChangesAsync();

            var result = validator.TestValidate(
                new SampleInputModel
                {
                    Name = data.Name
                });
            result.IsValid.Should()
                  .BeTrue();
        }
    }
}
