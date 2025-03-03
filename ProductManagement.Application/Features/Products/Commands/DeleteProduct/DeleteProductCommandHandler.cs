using AutoMapper;
using MediatR;
using ProductManagement.Application.Common.Exceptions;
using ProductManagement.Application.Contracts.Persistence;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public DeleteProductCommandHandler(IProductRepository productRepository,IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                throw new NotFoundException(nameof(Product),request.Id);
            }
            await _productRepository.DeleteAsync(product);
        }
    }
}
