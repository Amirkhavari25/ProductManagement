using ProductManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public string ManufactureEmail { get; set; } = string.Empty;
        public string ManufacturePhone { get; set; } = string.Empty;
        public DateTime ProduceDate { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
    }
}
