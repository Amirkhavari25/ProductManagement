using AutoMapper;
using MediatR;
using ProductManagement.Application.Common.Exceptions;
using ProductManagement.Application.Contracts.Persistence;
using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDTO?>
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<ProductDTO?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                throw new NotFoundException(nameof(product), product.ID);
            }
            return _mapper.Map<ProductDTO>(product);
        }
    }
}
