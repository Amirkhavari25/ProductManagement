using MediatR;
using ProductManagement.Application.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Features.Users.LoginCommand
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUserService _userService;
        public LoginCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
           return await _userService.LoginAsync(request.Email, request.Password);
        }
    }
}
