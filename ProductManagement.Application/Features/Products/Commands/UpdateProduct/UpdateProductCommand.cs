using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ManufactureEmail { get; set; }
        public string ManufacturePhone { get; set; }
        public DateTime ProduceDate { get; set; }
        public bool IsAvailable { get; set; }
    }
}
