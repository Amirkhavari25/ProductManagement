using MediatR;
using ProductManagement.Application.Contracts.Persistence;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand() : IRequest<Guid>;

}
