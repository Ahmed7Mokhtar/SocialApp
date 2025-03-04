using Application.Features.Accounts.Commands;
using Application.Features.Accounts.DTOs;
using Application.Features.Users.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController : BaseController
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register(RegisterCommand registerCommand)
    {
        var result = await _mediator.Send(registerCommand);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginCommand loginCommand)
    {
        var result = await _mediator.Send(loginCommand);
        return Ok(result);
    }
}
