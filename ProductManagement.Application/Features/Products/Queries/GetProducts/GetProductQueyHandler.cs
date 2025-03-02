using AutoMapper;
using MediatR;
using ProductManagement.Application.Contracts.Persistence;
using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Features.Products.Queries.GetProducts
{
    public class GetProductQueyHandler : IRequestHandler<GetProductQuery, List<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetProductQueyHandler(IProductRepository productRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public Task<List<ProductDTO>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var products = _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductDTO>>(products);
        }
    }
}
