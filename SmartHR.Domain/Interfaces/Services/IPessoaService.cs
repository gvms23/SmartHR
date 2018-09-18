using SmartHR.Domain.Entities;
using System.Collections.Generic;

namespace SmartHR.Domain.Interfaces.Services
{
    public interface IPessoaService : IServiceBase<Pessoa>
    {
        Pessoa ObterPessoaPorID(int id);

        List<Pessoa> ObterTodos();
    }
}