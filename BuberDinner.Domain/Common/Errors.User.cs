using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Common
{
    public static class Errors
    {
        public static class User
        {
            public static Error DuplicateEmail => Error.Conflict(
                code: "user duplicate email", description: "Email Already in use");
        }
    }
}
