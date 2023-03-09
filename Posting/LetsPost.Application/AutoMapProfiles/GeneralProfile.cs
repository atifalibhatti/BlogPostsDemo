using AutoMapper;
using LetsPost.Application.Command;
using LetsPost.Application.DTO;
using LetsPost.Domain.Documents;
using LetsPost.Domain.Entities;
using LetsPost.Domain.Events;

namespace LetsPost.Application.AutoMapProfiles;
public class GeneralProfile : Profile
{
    public GeneralProfile()
    {
        CreateMap<CreatePostCommand, Post>()
            .ForMember(d => d.CreatedAt, i => i.MapFrom(src => DateTime.Now));
        CreateMap<Post, PostDto>();
        CreateMap<UpdatePostCommand, Post>()
            .ForMember(d => d.Id, i => i.Ignore())
            .ForMember(d => d.CreatedAt, i => i.Ignore())
            .ForMember(d => d.UpdatedAt, i => i.MapFrom(src => DateTime.Now));

        CreateMap<Post, PostDocument>()
            .ForMember(d => d.Id, i => i.Ignore())
            .ForMember(d => d.PostId, i => i.MapFrom(src => src.Id));
    }
}
