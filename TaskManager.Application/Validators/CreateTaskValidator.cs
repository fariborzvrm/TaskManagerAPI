using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Validators
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskDto>
    {

        public CreateTaskValidator() {

            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100);
        
        }

    }
}
