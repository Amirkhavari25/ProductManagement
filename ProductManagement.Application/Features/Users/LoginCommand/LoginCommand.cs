using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Features.Users.LoginCommand
{
    public class LoginCommand:IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
