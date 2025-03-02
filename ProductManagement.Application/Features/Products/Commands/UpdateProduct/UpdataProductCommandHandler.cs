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

namespace ProductManagement.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            _mapper.Map(request, product);

            await _productRepository.UpdateAsync(product);

            return product.ID;
        }
    }


}
