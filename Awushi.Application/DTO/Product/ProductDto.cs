﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Application.DTO.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Category {  get; set; }
        public int BrandId { get; set; }
        public string Brand { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public int Rating { get; set; }

        [Required]
        public string ImageName { get; set; }

        //[NotMapped]
        //public IFormFile ImageFile { get; set; }
    }
}
