using BuberDinner.Application.Authentication.Command.Register;
using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Services.Authentication.Commands;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Application.Services.Authentication.Queries;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BuberDinner.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
      
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async IActionResult Register(RegisterRequest request)
        {
            var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);

            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

            return authResult.MatchFirst(
                authResult => Ok(MapAuthResult(authResult)),
                firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description));

        }

        private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(
                authResult.User.Id,
                authResult.User.FirstName,
                authResult.User.LastName,
                authResult.User.Email,
                authResult.Token);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResult = _authenticationQueryService.Login(
             request.Email,
             request.Password);
            return authResult.MatchFirst(
                authResult => Ok(MApLoginResult(authResult)),
                firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description));

        }

        private static AuthenticationResponse MApLoginResult(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(
                authResult.User.Id,
                authResult.User.FirstName,
                authResult.User.LastName,
                authResult.User.Email,
                authResult.Token);
        }
    }
}
