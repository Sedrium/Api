using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Dto;
using TodoApi.Model;

namespace TodoApi.Validators
{
    public class ProductCreateInputModelValidator : AbstractValidator<ProductCreateInputModel>
    {
        public ProductCreateInputModelValidator(bool idSholdBeEmpty)
        {
            if (idSholdBeEmpty)
                RuleFor(s => s.Id).Must(guid => guid.ToString().Equals("00000000-0000-0000-0000-000000000000"));
            else
                RuleFor(s => s.Id).Must(guid => !(guid.ToString().Equals("00000000-0000-0000-0000-000000000000")));
            RuleFor(s => s.Name).NotEmpty().MaximumLength(100);
            RuleFor(s => s.Price).NotNull().NotEqual(0);
        }
    }
}
