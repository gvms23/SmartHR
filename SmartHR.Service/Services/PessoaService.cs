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
    }
}