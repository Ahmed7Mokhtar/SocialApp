using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Application.Features.Accounts.DTOs;
using Application.Features.Users.DTOs;
using Application.ServiceInterfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Accounts.Commands;

public class RegisterCommand : IRequest<UserDTO>
{
    [Required(ErrorMessage = "Username is required!")]
    public string UserName { get; set; } = null!;
    [Required(ErrorMessage = "Password is required!")]
    [StringLength(8, MinimumLength = 4)]
    public string Password { get; set; } = null!;
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserDTO>
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;

    public RegisterCommandHandler(DataContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<UserDTO> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if(await UserExist(request.UserName))
            throw new ArgumentException("Username is taken");

        using var hmac = new HMACSHA512();
        var user = new AppUser
        {
            UserName = request.UserName.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
            PasswordSalt = hmac.Key
        };

        _context.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        return new UserDTO 
        {
            Username = user.UserName,
            Token = _tokenService.GenerateToken(user)
        };
    }

    private async Task<bool> UserExist(string username)
    {
        return await _context.AppUsers.AnyAsync(m => m.UserName == username.ToLower());
    }
}
