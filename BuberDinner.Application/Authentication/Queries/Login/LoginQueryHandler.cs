using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.persistence;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Domain.Common;
using BuberDinner.Domain.Entities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Authentication.Queries.Login
{
    internal class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
           _userRepository = userRepository;
        }
        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            // validate that the user is already exits  
            if (_userRepository.GetUserByEmail(query.Email) is not User user)
            {
                return new[] { LoginErrors.Authentication.InvalidCreditials };
            }
            if (user.Password != query.Password)
            {
                return LoginErrors.Authentication.InvalidCreditials;

            }
            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user,
                token);
        }
    }
}
