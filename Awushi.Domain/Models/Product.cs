using Awushi.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Domain.Models
{
    public class Product : BaseModel
    {
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int Stock {  get; set; }
        public int Rating { get; set; }
    }
}
