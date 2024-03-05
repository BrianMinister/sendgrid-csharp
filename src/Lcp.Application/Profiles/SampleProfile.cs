using Lcp.Application.InputModels;
using Lcp.Domain.Entities;

namespace Lcp.Application.Profiles
{
    /// <summary>
    ///     Provides the mapping profile for the sample.
    ///     <para>
    ///         References:<br />
    ///         <a href="http://docs.automapper.org/en/stable/Configuration.html"/>
    ///     </para>
    /// </summary>
    public class SampleProfile : Profile
    {
        /// <summary>
        ///     Initializes a new instance of the 
        ///     <see cref="SampleProfile"/> 
        ///     class.
        /// </summary>
        public SampleProfile()
        {
            CreateMap<SampleInputModel, Sample>();
        }
    }
}
