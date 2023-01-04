using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Common
{
    public static class LoginErrors
    {
        public static class Authentication
        {
            public static Error InvalidCreditials  => Error.Conflict(
                code: "auth invalid credintials", description: "user invalid credintials");
        }
    }
}
