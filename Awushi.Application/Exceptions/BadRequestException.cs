﻿using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Application.Exceptions
{
    public class BadRequestException :Exception
    {
        public IDictionary<string, string[]> ValidationError { get; set; }

        public BadRequestException(string message) : base(message)
        {
            
        }
        public BadRequestException(string message, ValidationResult validationResult) : base(message)
        {
            ValidationError = validationResult.ToDictionary();
        }
    }
}
