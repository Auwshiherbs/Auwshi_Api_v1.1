using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Application.DTO.Product
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int Stock { get; set; }
        public int Rating { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
