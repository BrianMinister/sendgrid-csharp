using FluentValidation;
using Lcp.Application.ErrorHandlers.Models;
using Lcp.Application.InputModels;
using Lcp.Infrastructure.EF.Contexts;
using System;
using System.Linq;

namespace Lcp.Application.Validators
{
    /// <summary>
    ///     Provides validation for the
    ///     sample input model.
    ///     <para>
    ///         References:<br />
    ///         <a href="https://fluentvalidation.net/"/>
    ///     </para>
    /// </summary>
    public class SampleValidator : AbstractValidator<SampleInputModel>
    {
        private readonly TemplateContext _dbContext;

        /// <summary>
        ///     Initializes a new instance of the 
        ///     <see cref="SampleValidator"/> 
        ///     class.
        /// </summary>
        /// <param name="dbContext">The database context factory.</param>
        /// <param name="id">The input id.</param>
        public SampleValidator(TemplateContext dbContext, Guid? id = null)
        {
            _dbContext = dbContext;

            RuleFor(c => c.Name)
                .MaximumLength(50)
                .NotEmpty()
                .Must((input, _) => NotBeDuplicateName(id, input))
                    .WithMessage(x => $"'Name' already exists.")
                    .WithErrorCode(ErrorCode.DUPLICATE_ERROR);
        }

        private bool NotBeDuplicateName(Guid? id, SampleInputModel input) =>
            !_dbContext.Samples.Any(
                x => x.Id != id && x.Name == input.Name
            );
    }
}
