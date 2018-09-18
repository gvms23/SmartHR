using System.Collections.Generic;
using SmartHR.Domain.Entities;

namespace SmartHR.Domain.Interfaces.Services
{
    public interface IVagaService : IServiceBase<Vaga>
    {
        Vaga ObterPorID(int id);
        List<Vaga> ObterTodos();
    }
}