using AutoMapper;
using HBM.Application.Common.Mappings;
using HBM.Application.Posts.Commands.CreatePost;
using System.ComponentModel.DataAnnotations;

namespace HBM.WebAPI.Models.Post
{
    public class CreatePostDto : IMapWith<CreatePostCommand>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Details { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePostDto, CreatePostCommand>()
                .ForMember(postCommand => postCommand.Title,
                opt => opt.MapFrom(postDto => postDto.Title))
                .ForMember(postCommand => postCommand.Details,
                opt => opt.MapFrom(postDto => postDto.Details));
        }
    }
}