using System;
using AutoMapper;
using StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB.Model;
using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Dispatchers.MappingProfiles
{
    public class TheMovieDbMappingProfile:Profile
    {
        public TheMovieDbMappingProfile()
        {
            CreateMap<TelevisionResult, TelevisionResultDto>()
                .ForMember(destination => destination.FirstAirDate, options => options.MapFrom(source => !string.IsNullOrWhiteSpace(source.FirstAirDate) ? DateTime.Parse(source.FirstAirDate) : (DateTime?)null));
            CreateMap<TelevisionSearchResponse, TelevisionSearchResponseDto>();
        }
    }
}
