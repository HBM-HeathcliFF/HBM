using AutoMapper;
using HBM.Application.Common.Exceptions;
using HBM.Application.Interfaces;
using HBM.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HBM.Application.Posts.Queries.GetPostDetails
{
    public class GetPostQueryHandler : IRequestHandler<GetPostQuery, PostVm>
    {
        private readonly IHbmDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPostQueryHandler(IHbmDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<PostVm> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Posts
                .FirstOrDefaultAsync(post => post.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Post), request.Id);
            }

            return _mapper.Map<PostVm>(entity);
        }
    }
}