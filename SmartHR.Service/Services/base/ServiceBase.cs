using FluentValidation;
using SmartHR.Domain.Entities;
using SmartHR.Domain.Interfaces;
using SmartHR.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHR.Service.Services
{
    public class ServiceBase<T> : IServiceBase<T> where T : class
    {
        private IRepositoryBase<T> _repository;

        public ServiceBase(IRepositoryBase<T> repository)
        {
            _repository = repository;
        }

        public T Inserir<V>(T obj) where V : AbstractValidator<T>
        {
            Validate(obj, Activator.CreateInstance<V>());

            _repository.Inserir(obj);
            return obj;
        }

        public List<T> InserirRange<V>(List<T> objs) where V : AbstractValidator<T>
        {
            objs.ForEach(o => Validate(o, Activator.CreateInstance<V>()));

            _repository.InserirRange(objs);
            return objs;
        }

        public T InserirSemValidacao(T obj)
        {
            _repository.Inserir(obj);
            return obj;
        }

        public T Atualizar<V>(T obj) where V : AbstractValidator<T>
        {
            if ((int)obj.GetType().GetProperty("Id").GetValue(obj) == 0)
                throw new Exception("ID não pode ser nulo ou zero.");

            Validate(obj, Activator.CreateInstance<V>());

            _repository.Atualizar(obj);
            return obj;
        }

        public void Excluir(int id)
        {
            if (id == 0)
                throw new ArgumentException("O ID não deve ser zero.");

            _repository.Excluir(id);
        }

        public IList<T> Obter() => _repository.Obter();

        public T Obter(int id)
        {
            if (id == 0)
                throw new ArgumentException("O ID não deve ser zero.");

            return _repository.Obter(id);
        }

        private void Validate(T obj, AbstractValidator<T> validator)
        {
            if (obj == null)
                throw new Exception("Registro não detectado.");

            validator.ValidateAndThrow(obj);
        }
    }
}
