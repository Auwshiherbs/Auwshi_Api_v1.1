using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Application.DTO.Brand
{
    public class UpdateBrandDto
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public int EstablishedYear { get; set; }
    }
}
