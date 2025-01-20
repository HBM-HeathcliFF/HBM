using AutoMapper;
using HBM.Application.Common.Mappings;
using HBM.Domain;

namespace HBM.Application.Posts.Queries.GetPostList
{
    public class PostLookupDto : IMapWith<Post>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Post, PostLookupDto>()
                .ForMember(postDto => postDto.Id,
                    opt => opt.MapFrom(post => post.Id))
                .ForMember(postDto => postDto.Title,
                    opt => opt.MapFrom(post => post.Title))
                .ForMember(postDto => postDto.Details,
                    opt => opt.MapFrom(post => post.Details))
                .ForMember(postDto => postDto.CreationDate,
                    opt => opt.MapFrom(post => post.CreationDate))
                .ForMember(postDto => postDto.UserName,
                    opt => opt.MapFrom(post => post.AppUser.UserName));
        }
    }
}