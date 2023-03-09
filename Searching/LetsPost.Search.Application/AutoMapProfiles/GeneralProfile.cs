using AutoMapper;
using LetsPost.Search.Application.DTO;
using LetsPost.Search.Domain.Documents;

namespace LetsPost.Search.Application.AutoMapProfiles;
public class GeneralProfile : Profile
{
    public GeneralProfile()
    {
        CreateMap<PostDocument, PostDto>();
    }
}
