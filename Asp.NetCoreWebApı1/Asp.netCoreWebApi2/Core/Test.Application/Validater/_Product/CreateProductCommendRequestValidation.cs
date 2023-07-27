using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Application.Features.Commends._Product.CreateProduct;

namespace Test.Application.Validater._Product
{
    public class CreateProductCommendRequestValidation : AbstractValidator<CreateProductCommendRequest>
    {
        public CreateProductCommendRequestValidation()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("İsim boş geçilemez")
                .MinimumLength(2).WithMessage("Mın 2 karakterli olmalıdr");
            RuleFor(x => x.Price).NotNull().NotEmpty().WithMessage("Fıyat bos veya null olamaz")
                .Must(i => i >= 0 && i <= 10000).WithMessage("Fıyat bılgısı 0 dan kucuk ve 10000 den buyuk olamaz");

        }
    }
}
