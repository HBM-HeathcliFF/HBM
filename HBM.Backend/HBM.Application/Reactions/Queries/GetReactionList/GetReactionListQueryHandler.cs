using AutoMapper;
using AutoMapper.QueryableExtensions;
using HBM.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HBM.Application.Reactions.Queries.GetReactionList
{
    public class GetReactionListQueryHandler : IRequestHandler<GetReactionListQuery, ReactionListVm>
    {
        private readonly IHbmDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetReactionListQueryHandler(IHbmDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<ReactionListVm> Handle(GetReactionListQuery request, CancellationToken cancellationToken)
        {
            var reactionsQuery = await _dbContext.Reactions
                .Where(reaction => reaction.PostId == request.PostId)
                .ProjectTo<ReactionLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ReactionListVm { Reactions = reactionsQuery };
        }
    }
}