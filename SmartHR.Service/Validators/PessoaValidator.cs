using FluentValidation;
using SmartHR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHR.Service.Validators
{
    public class PessoaValidator : AbstractValidator<Pessoa>
    {
        public PessoaValidator()
        {
            RuleFor(p => p)
                .NotNull()
                .OnAnyFailure(x =>
                {
                    throw new ArgumentNullException("Objeto não encontrato.");
                });

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("É necesssário informar o nome.")
                .NotNull().WithMessage("É necesssário informar o nome.");

            RuleFor(c => c.Profissao)
                .NotEmpty().WithMessage("É necesssário informar a profissão.")
                .NotNull().WithMessage("É necesssário informar a profissão.");

            RuleFor(c => c.Localizacao)
                .NotEmpty().WithMessage("É necesssário informar a localização.")
                .NotNull().WithMessage("É necesssário informar a localização.");

            RuleFor(c => c.Nivel)
                .NotEmpty().WithMessage("É necesssário informar o nível.")
                .NotNull().WithMessage("É necesssário informar o nível.");
        }
    }
}