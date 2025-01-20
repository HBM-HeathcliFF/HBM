using AutoMapper;
using AutoMapper.QueryableExtensions;
using HBM.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HBM.Application.Comments.Queries.GetCommentList
{
    public class GetCommentListQueryHandler : IRequestHandler<GetCommentListQuery, CommentListVm>
    {
        private readonly IHbmDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCommentListQueryHandler(IHbmDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<CommentListVm> Handle(GetCommentListQuery request, CancellationToken cancellationToken)
        {
            var commentsQuery = await _dbContext.Comments
                .Where(comment => comment.PostId == request.PostId)
                .ProjectTo<CommentLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CommentListVm { Comments = commentsQuery };
        }
    }
}