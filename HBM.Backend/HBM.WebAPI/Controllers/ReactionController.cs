﻿using Asp.Versioning;
using AutoMapper;
using HBM.Application.AppUsers.Commands.CreateAppUser;
using HBM.Application.Interfaces;
using HBM.Application.Reactions.Commands.CreateReaction;
using HBM.Application.Reactions.Commands.DeleteReaction;
using HBM.Application.Reactions.Queries.GetReactionList;
using HBM.WebAPI.Models.AppUser;
using HBM.WebAPI.Models.Reaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HBM.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/[controller]")]
    public class ReactionController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public ReactionController(IMapper mapper, ICurrentUserService currentUserService) =>
            (_mapper, _currentUserService) = (mapper, currentUserService);

        /// <summary>
        /// Get list of reactions
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /reaction/25FFCB91-AB0A-4A24-9108-EC94F8BE000A
        /// </remarks>
        /// <param name="postId">Post id (guid)</param>
        /// <returns>Returns ReactionListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet("{postId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReactionListVm>> GetAll(Guid postId)
        {
            var query = new GetReactionListQuery
            {
                PostId = postId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the reaction
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /reaction
        /// {
        ///     postId: "id of the post",
        ///     isLiked: "type of reaction"
        /// }
        /// </remarks>
        /// <param name="createReactionDto">CreateReactionDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateReactionDto createReactionDto)
        {
            var createAppUserDto = new CreateAppUserDto
            {
                Id = _currentUserService.UserId,
                UserName = _currentUserService.UserName,
                Role = _currentUserService.Role
            };

            var command1 = _mapper.Map<CreateAppUserCommand>(createAppUserDto);
            await Mediator.Send(command1);

            var command2 = _mapper.Map<CreateReactionCommand>(createReactionDto);
            command2.UserId = _currentUserService.UserId;
            var commentId = await Mediator.Send(command2);
            return Ok(commentId);
        }

        /// <summary>
        /// Deletes the reaction by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /reaction/20DC3FB7-BFA9-40AE-8CEF-12BC6F31DD79
        /// </remarks>
        /// <param name="id">Id of the reaction (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteReactionCommand
            {
                Id = id,
                UserId = _currentUserService.UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
