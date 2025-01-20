using AutoMapper;
using HBM.Application.Comments.Commands.UpdateComment;
using HBM.Application.Common.Mappings;

namespace HBM.WebAPI.Models.Comment
{
    public class UpdateCommentDto : IMapWith<UpdateCommentCommand>
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public string Text { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCommentDto, UpdateCommentCommand>()
                .ForMember(commentCommand => commentCommand.PostId,
                opt => opt.MapFrom(commentDto => commentDto.PostId))
                .ForMember(commentCommand => commentCommand.Id,
                opt => opt.MapFrom(commentDto => commentDto.Id))
                .ForMember(commentCommand => commentCommand.Text,
                opt => opt.MapFrom(commentDto => commentDto.Text));
        }
    }
}