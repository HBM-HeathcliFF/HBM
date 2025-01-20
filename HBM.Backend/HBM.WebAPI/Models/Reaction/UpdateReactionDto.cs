using AutoMapper;
using HBM.Application.Common.Mappings;
using HBM.Application.Reactions.Commands.UpdateReaction;

namespace HBM.WebAPI.Models.Reaction
{
    public class UpdateReactionDto : IMapWith<UpdateReactionCommand>
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateReactionDto, UpdateReactionCommand>()
                .ForMember(reactionCommand => reactionCommand.PostId,
                opt => opt.MapFrom(reactionDto => reactionDto.PostId))
                .ForMember(reactionCommand => reactionCommand.Id,
                opt => opt.MapFrom(reactionDto => reactionDto.Id));
        }
    }
}
