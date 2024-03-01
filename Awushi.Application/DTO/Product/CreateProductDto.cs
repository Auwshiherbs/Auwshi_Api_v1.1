﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Application.DTO.Product
{
    public class CreateProductDto
    {
        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int Stock { get; set; }
    }
}
