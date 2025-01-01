using System;
using Domain.Entities;

namespace Application.ServiceInterfaces;

public interface ITokenService
{
    string GenerateToken(AppUser user);
}
