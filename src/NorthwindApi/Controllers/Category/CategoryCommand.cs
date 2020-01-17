using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NorthwindApi.Controllers
{
    public class CreateCategoryCommand : IRequest<IActionResult>
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }

    public class UpdateCategoryCommand : IRequest<IActionResult>
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }

    public class DeleteCategoryCommand : IRequest<IActionResult>
    {
        public int CategoryId { get; set; }
    }

    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            var msgError1 = "'{PropertyName}' tidak boleh kosong !";
            var msgError2 = "Inputan '{PropertyName}' maksimal {MaxLength} karakter !";

            RuleFor(c => c.CategoryName).NotEmpty().WithMessage(msgError1).Length(1, 10).WithMessage(msgError2);
            RuleFor(c => c.Description).NotEmpty().WithMessage(msgError1).Length(1, 20).WithMessage(msgError2);
        }
    }

    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            var msgError1 = "'{PropertyName}' tidak boleh kosong !";
            var msgError2 = "Inputan '{PropertyName}' maksimal {MaxLength} karakter !";

            RuleFor(c => c.CategoryName).NotEmpty().WithMessage(msgError1).Length(1, 10).WithMessage(msgError2);
            RuleFor(c => c.Description).NotEmpty().WithMessage(msgError1).Length(1, 20).WithMessage(msgError2);
        }
    }
}
