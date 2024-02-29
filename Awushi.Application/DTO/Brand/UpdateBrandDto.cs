using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Application.DTO.Brand
{
    public class UpdateBrandDto
    {
        public string Name { get; set; }
        public DateTime EstablishedYear { get; set; } = DateTime.UtcNow;
    }
}
