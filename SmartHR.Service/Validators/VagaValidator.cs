using FluentValidation;
using SmartHR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHR.Service.Validators
{
    public class VagaValidator : AbstractValidator<Vaga>
    {
        public VagaValidator()
        {
            RuleFor(o => o)
                .NotNull()
                .OnAnyFailure(x =>
                {
                    throw new ArgumentNullException("Objeto não encontrato.");
                });

            RuleFor(c => c.Empresa)
                .NotEmpty().WithMessage("É necesssário informar a empresa.")
                .NotNull().WithMessage("É necesssário informar a empresa.");

            RuleFor(c => c.Titulo)
                .NotEmpty().WithMessage("É necesssário informar o título.")
                .NotNull().WithMessage("É necesssário informar o título.");

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("É necesssário informar a descrição.")
                .NotNull().WithMessage("É necesssário informar a descrição.");

            RuleFor(c => c.Localizacao)
                .NotEmpty().WithMessage("É necesssário informar a localização.")
                .NotNull().WithMessage("É necesssário informar a localização.");
            
            RuleFor(c => c.Nivel)
                .NotEmpty().WithMessage("É necesssário informar o nível.")
                .NotNull().WithMessage("É necesssário informar o nível.");
        }
    }
}