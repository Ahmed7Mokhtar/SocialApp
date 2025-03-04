using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Application.Features.Accounts.DTOs;
using Application.ServiceInterfaces;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Accounts.Commands;

public class LoginCommand : IRequest<UserDTO>
{
    [Required(ErrorMessage = "Username is required!")]
    public string UserName { get; set; } = null!;
    [Required(ErrorMessage = "Password is required!")]
    public string Password { get; set; } = null!;
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, UserDTO>
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public LoginCommandHandler(ITokenService tokenService, IUserRepository userRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    public async Task<UserDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.UserName) ?? throw new ArgumentException("Invalid username");
        
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
        for(int i = 0; i < computedHash.Length; i++)
        {
            if(computedHash[i] != user.PasswordHash[i])
                throw new ArgumentException("Invalid password");
        }

        return new UserDTO 
        {
            Username = user.UserName,
            Token = _tokenService.GenerateToken(user)
        };
    }
}
