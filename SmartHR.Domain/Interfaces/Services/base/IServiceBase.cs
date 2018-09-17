using FluentValidation;
using SmartHR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHR.Domain.Interfaces
{
    public interface IServiceBase<T> where T : class
    {
        T Inserir<V>(T obj) where V : AbstractValidator<T>;

        List<T> InserirRange<V>(List<T> objs) where V : AbstractValidator<T>;

        T Atualizar<V>(T obj) where V : AbstractValidator<T>;

        void Excluir(int id);

        T Obter(int id);

        IList<T> Obter();
    }
}
