﻿using Asp.Versioning;
using AutoMapper;
using HBM.Application.AppUsers.Commands.CreateAppUser;
using HBM.Application.Comments.Commands.CreateComment;
using HBM.Application.Comments.Commands.DeleteComment;
using HBM.Application.Comments.Commands.UpdateComment;
using HBM.Application.Comments.Queries.GetCommentList;
using HBM.Application.Interfaces;
using HBM.WebAPI.Models.AppUser;
using HBM.WebAPI.Models.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HBM.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/[controller]")]
    public class CommentController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IHbmDbContext _dbContext;

        public CommentController(IMapper mapper, ICurrentUserService currentUserService, IHbmDbContext dbContext) =>
            (_mapper, _currentUserService, _dbContext) = (mapper, currentUserService, dbContext);

        /// <summary>
        /// Get list of comments
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /comment/25FFCB91-AB0A-4A24-9108-EC94F8BE000A
        /// </remarks>
        /// <param name="postId">Post id (guid)</param>
        /// <returns>Returns CommentListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("{postId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CommentListVm>> GetAll(Guid postId)
        {
            var query = new GetCommentListQuery
            {
                PostId = postId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the comment
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /comment
        /// {
        ///     postId: "id of the post",
        ///     text: "comment text"
        /// }
        /// </remarks>
        /// <param name="createCommentDto">CreateCommentDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="201">Success</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateCommentDto createCommentDto)
        {
            var createAppUserDto = new CreateAppUserDto
            {
                Id = _currentUserService.UserId,
                UserName = _currentUserService.UserName,
                Role = _currentUserService.Role
            };

            var command1 = _mapper.Map<CreateAppUserCommand>(createAppUserDto);
            await Mediator.Send(command1);

            var command2 = _mapper.Map<CreateCommentCommand>(createCommentDto);
            command2.UserId = _currentUserService.UserId;
            var commentId = await Mediator.Send(command2);
            return Ok(commentId);
        }

        /// <summary>
        /// Updates the comment
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /comment
        /// {
        ///     text: "updated comment text"
        /// }
        /// </remarks>
        /// <param name="updateCommentDto">UpdateCommentDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Update([FromBody] UpdateCommentDto updateCommentDto)
        {
            var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == updateCommentDto.Id);

            if (_currentUserService.UserId == comment?.UserId ||
                _currentUserService.Role == "Admin" ||
                _currentUserService.Role == "Owner")
            {
                var command = _mapper.Map<UpdateCommentCommand>(updateCommentDto);
                command.UserId = _currentUserService.UserId;
                await Mediator.Send(command);
            }
            
            return NoContent();
        }

        /// <summary>
        /// Deletes the comment by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /comment/20DC3FB7-BFA9-40AE-8CEF-12BC6F31DD79
        /// </remarks>
        /// <param name="id">Id of the comment (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (_currentUserService.UserId == comment?.UserId ||
                _currentUserService.Role == "Admin" ||
                _currentUserService.Role == "Owner")
            {
                var command = new DeleteCommentCommand
                {
                    Id = id,
                    UserId = comment.UserId,
                    PostId = comment.PostId
                };
                await Mediator.Send(command);
            }
            
            return NoContent();
        }
    }
}