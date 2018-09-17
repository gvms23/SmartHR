using System.Collections.Generic;
using System.Linq;
using SmartHR.Domain.Entities;
using SmartHR.Domain.Interfaces.Repositories;
using SmartHR.Domain.Interfaces.Services;

namespace SmartHR.Service.Services
{
    public class VagaService : ServiceBase<Vaga>, IVagaService
    {
        private IVagaRepository _repository;

        public VagaService(IVagaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

    }
}