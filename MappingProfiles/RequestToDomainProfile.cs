using apiServices.Data.Queries;
using apiServices.Domain;
using AutoMapper;

namespace apiServices.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
        }
    }
}
