using FluentValidation;
using SmartHR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHR.Service.Validators
{
    public class CandidaturaValidator : AbstractValidator<Candidatura>
    {
        public CandidaturaValidator()
        {
            RuleFor(p => p)
                .NotNull()
                .OnAnyFailure(x =>
                {
                    throw new ArgumentNullException("Objeto não encontrato.");
                });

            RuleFor(c => c.PessoaID)
                .NotEmpty().WithMessage("É necesssário informar a pessoa.")
                .NotNull().WithMessage("É necesssário informar a pessoa.");

            RuleFor(c => c.VagaID)
                .NotEmpty().WithMessage("É necesssário informar a vaga.")
                .NotNull().WithMessage("É necesssário informar a vaga.");

            RuleFor(c => c.Score)
                .NotEmpty().WithMessage("É necesssário que o Score seja calculado.")
                .NotNull().WithMessage("É necesssário que o Score seja calculado.");
        }
    }
}