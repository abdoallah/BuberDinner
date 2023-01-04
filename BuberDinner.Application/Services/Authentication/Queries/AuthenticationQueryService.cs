using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.persistence;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Domain.Common;
using BuberDinner.Domain.Entities;
using ErrorOr;
using FluentResults;


namespace BuberDinner.Application.Services.Authentication.Queries;

public class AuthenticationQueryService : IAuthenticationQueryService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository = null)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        // validate that the user is already exits  
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            return new[] { Errors.Authentication.InvalidCreditials };
        }
        if (user.Password != password)
        {
            return Errors.Authentication.InvalidCreditials;

        }
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(user,
            token);
    }

}