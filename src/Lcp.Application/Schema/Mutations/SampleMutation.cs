using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using Lcp.Application.ErrorHandlers.Helpers;
using Lcp.Application.InputModels;
using Lcp.Application.Payloads;
using Lcp.Application.Payloads.Models;
using Lcp.Application.Validators;
using Lcp.Domain.Entities;
using Lcp.Infrastructure.EF.Contexts;
using Lcp.Microsvs.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Lcp.Application.Schema.Mutations
{
    /// <summary>
    ///     Provides the mutations for
    ///     the sample.
    /// </summary>

    [ExtendObjectType(Name = "TemplateMutation")]
    public class SampleMutation
    {
        /// <summary>
        ///     Creates a new sample.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="httpAccessor">The http context accessor.</param>
        /// <param name="mapper">The AutoMapper mapper.</param>
        /// <param name="eventsSender">The event sender.</param>
        /// <param name="input">The sample input model.</param>
        /// <returns>The new sample.</returns>
        [Authorize]
        [UseDbContext(typeof(TemplateContext))]
        public async Task<Payload<Sample>> CreateSample(
            [ScopedService] TemplateContext dbContext,
            [Service] IHttpContextAccessor httpAccessor,
            [Service] IEventSender eventsSender,
            SampleInputModel input)
        {
            var result = new SampleValidator(dbContext)
                                    .Validate(input);

            if (!result.IsValid)
                return new Payload<Sample>(result.FormatErrors());

            //var entityEntry = await dbContext.AddAsync(
            //                        mapper.Map<SampleInputModel, Sample>(input)
            //                    );

            //await dbContext.SaveChangesAsync<Sample>(
            //    httpAccessor.HttpContext,
            //    eventsSender);

            //return new Payload<Sample>(entityEntry.Entity);
        }

        /// <summary>
        ///     Updates an existing sample.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="httpAccessor">The http context accessor.</param>
        /// <param name="eventsSender">The event sender.</param>
        /// <param name="id">The sample id.</param>
        /// <param name="input">The sample input model.</param>
        /// <returns>The updated sample.</returns>
        [Authorize]
        [UseDbContext(typeof(TemplateContext))]
        public async Task<Payload<Sample>> UpdateSample(
            [ScopedService] TemplateContext dbContext,
            [Service] IHttpContextAccessor httpAccessor,
            [Service] IEventSender eventsSender,
            Guid id,
            SampleInputModel input)
        {

            var entity = await dbContext.Samples
                                        .FindAsync(id);

            if (entity == null)
                return new Payload<Sample>(
                    ErrorHelper.GetIdNotFoundError(id)
                );

            var result = new SampleValidator(dbContext, id)
                               .Validate(input);

            if (!result.IsValid)
                return new Payload<Sample>(result.FormatErrors());

            mapper.Map(input, entity);
            dbContext.Update(entity);

            await dbContext.SaveChangesAsync<Sample>(
                httpAccessor.HttpContext,
                eventsSender);

            return new Payload<Sample>(entity);
        }

        /// <summary>
        ///     Deletes an existing sample.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="httpAccessor">The http context accessor.</param>
        /// <param name="eventsSender">The event sender.</param>
        /// <param name="id">The sample id.</param>
        /// <returns>The delete success payload.</returns>
        [Authorize]
        [UseDbContext(typeof(TemplateContext))]
        public async Task<Payload<DeleteSuccess>> DeleteSample(
            [ScopedService] TemplateContext dbContext,
            [Service] IHttpContextAccessor httpAccessor,
            [Service] IEventSender eventsSender,
            Guid id)
        {
            var entity = await dbContext.Samples
                                        .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return new Payload<DeleteSuccess>(
                    ErrorHelper.GetIdNotFoundError(id)
                );

            entity.IsDeleted = true;

            await dbContext.SaveChangesAsync<Sample>(
                httpAccessor.HttpContext,
                eventsSender);

            return new Payload<DeleteSuccess>(new DeleteSuccess(true));
        }
    }
}
