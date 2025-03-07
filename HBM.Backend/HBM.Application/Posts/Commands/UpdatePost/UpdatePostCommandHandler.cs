﻿using HBM.Application.Common.Exceptions;
using HBM.Application.Extensions;
using HBM.Application.Interfaces;
using HBM.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HBM.Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Unit>
    {
        private readonly IHbmDbContext _dbContext;

        public UpdatePostCommandHandler(IHbmDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Posts.FirstOrDefaultAsync(post =>
                post.Id == request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Post), request.Id);
            }

            entity.Text = request.Text;
            entity.Title = request.Title;
            entity.EditDate = DateTime.Now.ToCommentFormat();

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}