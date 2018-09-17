using SmartHR.Domain.Entities;
using SmartHR.Domain.Interfaces.Repositories;
using SmartHR.Infra.Data.Context;

namespace SmartHR.Infra.Data.Repository
{
    public class PessoaRepository : RepositoryBase<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(SmartHRContext context) : base(context) { }
    }
}