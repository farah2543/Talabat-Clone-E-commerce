using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class UnauthorizedException(string msg = "Invalid E-mail or Password" ) :Exception (msg)
    {
    }
}
