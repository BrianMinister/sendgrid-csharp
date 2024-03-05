using Lcp.Application.InputModels;
using Lcp.Domain.Entities;

namespace Lcp.Application.Profiles
{
    /// <summary>
    ///     Provides the mapping profile for the base.
    ///     <para>
    ///         References:<br />
    ///         <a href="http://docs.automapper.org/en/stable/Configuration.html"/>
    ///     </para>
    /// </summary>
    public class BaseProfile : Profile
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="BaseProfile"/> class.
        /// </summary>
        public BaseProfile()
        {
            CreateMap<BaseInputModel, BaseEntity>()
                .IncludeAllDerived()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.IsDeleted, opt => opt.Ignore());
        }
    }
}
