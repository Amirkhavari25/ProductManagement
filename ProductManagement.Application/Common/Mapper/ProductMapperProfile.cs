using AutoMapper;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Features.Products.Commands.CreateProduct;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Common.Mapper
{
    public class ProductMapperProfile :Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<Product, ProductDTO>();
        }
    }
}
