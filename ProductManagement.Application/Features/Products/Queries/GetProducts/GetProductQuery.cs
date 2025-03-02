using MediatR;
using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Features.Products.Queries.GetProducts
{
    public record GetProductQuery:IRequest<List<ProductDTO>>;
    
}
