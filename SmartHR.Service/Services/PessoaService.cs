using System.Collections.Generic;
using System.Linq;
using SmartHR.Domain.Entities;
using SmartHR.Domain.Interfaces.Repositories;
using SmartHR.Domain.Interfaces.Services;

namespace SmartHR.Service.Services
{
    public class PessoaService : ServiceBase<Pessoa>, IPessoaService
    {
        private IPessoaRepository _repository;

        public PessoaService(IPessoaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public Pessoa ObterPessoaPorID(int id)
        {
            return _repository.Obter(wh => wh.Id == id, i => i.Candidaturas);
        }

        public List<Pessoa> ObterTodos()
        {
            return _repository.ObterTodos(null, i => i.Candidaturas).ToList();
        }
    }
}