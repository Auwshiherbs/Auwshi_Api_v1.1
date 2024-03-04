using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Application.DTO.Brand
{
    public class CreateBrandDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int EstablishedYear { get; set; }
    }

    public class CreateBrandDtoValidator : AbstractValidator<BrandDto>
    {
        public CreateBrandDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.EstablishedYear).InclusiveBetween(1980,DateTime.UtcNow.Year);
        }
    }
}
