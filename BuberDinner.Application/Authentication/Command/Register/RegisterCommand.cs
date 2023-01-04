using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Authentication.Command.Register
{
    public record RegisterCommand(string FirstName,
        string LastName,
        string Email, 
        string Password):IRequest<ErrorOr<AuthenticationResult>>;

}
