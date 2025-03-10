using Application.Features.Users.DTOs;
using Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
        {
            var query = new GetUsersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<UserDTO>> GetUserById(string id)
        //{
        //    var query = new GetUserByIdQuery(id);
        //    var result = await _mediator.Send(query);
        //    return Ok(result);
        //}

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDTO>> GetUserByUsername(string username)
        {
            var query = new GetUserByUsernameQuery(username);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
