using SmartHR.Domain.Entities;
using SmartHR.Domain.Interfaces.Repositories;
using SmartHR.Infra.Data.Context;

namespace SmartHR.Infra.Data.Repository
{
    public class VagaRepository : RepositoryBase<Vaga>, IVagaRepository
    {
        public VagaRepository(SmartHRContext context) : base(context) { }
    }
}