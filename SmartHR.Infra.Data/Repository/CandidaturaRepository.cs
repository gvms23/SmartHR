using SmartHR.Domain.Entities;
using SmartHR.Domain.Interfaces.Repositories;
using SmartHR.Infra.Data.Context;

namespace SmartHR.Infra.Data.Repository
{
    public class CandidaturaRepository : RepositoryBase<Candidatura>, ICandidaturaRepository
    {
        public CandidaturaRepository(SmartHRContext context) : base(context) { }
    }
}