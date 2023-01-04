using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.persistence;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Domain.Common;
using BuberDinner.Domain.Entities;
using ErrorOr;
using FluentResults;


namespace BuberDinner.Application.Services.Authentication.Commands;

public class AuthenticationCommandsService : IAuthenticationCommandService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationCommandsService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository = null)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        //1 TODO : check if the user us already exist 
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }
        // 2create user (generate unique id) and send user to the db 
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        _userRepository.Add(user);
        // create jwt token 
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(user, token);

    }
}