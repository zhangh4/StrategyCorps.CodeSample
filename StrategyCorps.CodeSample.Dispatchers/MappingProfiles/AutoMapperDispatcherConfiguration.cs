using AutoMapper;
using StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB.Model;
using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Dispatchers.MappingProfiles
{
    public class AutoMapperDispatcherConfiguration : Profile
    {

        public static void Configure()
        {
           // CreateMap<TelevisionSearchResponse, TelevisionSearchResponseDTO>();

            //.ForMember(destination => destination.Contact, options => options.MapFrom(source => new ContactModel { PhoneNumber = source.Contact.PhoneNumber, Email = source.Contact.Email }));
        }
    }
}
