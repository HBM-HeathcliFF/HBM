using AutoMapper;
using HBM.Application.Common.Mappings;
using HBM.Domain;

namespace HBM.Application.Posts.Queries.GetPostDetails
{
    public class PostVm : IMapWith<Post>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string CreationDate { get; set; }
        public string? EditDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Post, PostVm>()
                .ForMember(postVm => postVm.Title,
                    opt => opt.MapFrom(post => post.Title))
                .ForMember(postVm => postVm.Text,
                    opt => opt.MapFrom(post => post.Text))
                .ForMember(postVm => postVm.Id,
                    opt => opt.MapFrom(post => post.Id))
                .ForMember(postVm => postVm.CreationDate,
                    opt => opt.MapFrom(post => post.CreationDate))
                .ForMember(postVm => postVm.EditDate,
                    opt => opt.MapFrom(post => post.EditDate));
        }
    }
}