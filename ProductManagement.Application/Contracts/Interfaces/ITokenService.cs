using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Contracts.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(TokenPayloadDTO payload);
    }
}
