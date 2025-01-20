using AutoMapper;
using HBM.Application.Common.Mappings;
using HBM.Domain;

namespace HBM.Application.Reactions.Queries.GetReactionList
{
    public class ReactionLookupDto : IMapWith<Reaction>
    {
        public Guid Id { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Reaction, ReactionLookupDto>()
                .ForMember(reactionDto => reactionDto.Id,
                    opt => opt.MapFrom(reaction => reaction.Id));
        }
    }
}