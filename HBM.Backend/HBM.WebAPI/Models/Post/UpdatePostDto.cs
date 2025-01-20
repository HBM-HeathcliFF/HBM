using AutoMapper;
using HBM.Application.Common.Mappings;
using HBM.Application.Posts.Commands.UpdatePost;

namespace HBM.WebAPI.Models.Post
{
    public class UpdatePostDto : IMapWith<UpdatePostCommand>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdatePostDto, UpdatePostCommand>()
                .ForMember(postCommand => postCommand.Id,
                opt => opt.MapFrom(postDto => postDto.Id))
                .ForMember(postCommand => postCommand.Title,
                opt => opt.MapFrom(postDto => postDto.Title))
                .ForMember(postCommand => postCommand.Details,
                opt => opt.MapFrom(postDto => postDto.Details));
        }
    }
}