using AutoMapper;
using HBM.Application.Common.Mappings;
using HBM.Domain;

namespace HBM.Application.Comments.Queries.GetCommentList
{
    public class CommentLookupDto : IMapWith<Comment>
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string CreationDate { get; set; }
        public string UserName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Comment, CommentLookupDto>()
                .ForMember(commentDto => commentDto.Id,
                    opt => opt.MapFrom(comment => comment.Id))
                .ForMember(commentDto => commentDto.Text,
                    opt => opt.MapFrom(comment => comment.Text))
                .ForMember(commentDto => commentDto.CreationDate,
                    opt => opt.MapFrom(comment => comment.CreationDate))
                .ForMember(commentDto => commentDto.UserName,
                    opt => opt.MapFrom(comment => comment.AppUser.UserName));
        }
    }
}