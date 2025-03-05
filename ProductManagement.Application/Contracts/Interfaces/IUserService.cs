using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Contracts.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterAsync(string email, string password, string fullName);
        Task<string> LoginAsync(string email, string password); 
    }
}
