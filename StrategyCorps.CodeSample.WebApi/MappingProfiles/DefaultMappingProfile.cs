using AutoMapper;
using StrategyCorps.CodeSample.Models;
using StrategyCorps.CodeSample.WebApi.ViewModels;

namespace StrategyCorps.CodeSample.WebApi.MappingProfiles
{
    /// <summary>
    /// The WebApi default mapping profiles
    /// </summary>
    public class DefaultMappingProfile : Profile
    {
        /// <summary>
        /// The  WebApi default mapping profile constructor
        /// </summary>
        public DefaultMappingProfile()
        {
            CreateMap<TelevisionResultDto, TelevisionResultViewModel>()
                .ForMember(destination => destination.FirstAirDate, options => options.MapFrom(source => source.FirstAirDate != null ? source.FirstAirDate.ToString() : string.Empty));
            CreateMap<TelevisionSearchResponseDto, TelevisionSearchResponseViewModel>();
        }
    }
}
