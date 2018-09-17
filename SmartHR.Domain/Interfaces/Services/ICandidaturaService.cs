using System.Collections.Generic;
using SmartHR.Domain.Entities;
using SmartHR.Domain.ViewModels;

namespace SmartHR.Domain.Interfaces.Services
{
    public interface ICandidaturaService : IServiceBase<Candidatura>
    {
        List<Candidatura> ObterPorVagaID(int vagaID);

        List<Candidatura> ObterTodos();

        Candidatura ConverterInserir(CandidaturaVM candidaturaVM);

        bool Excluir(int pessoaID, int vagaID);

        List<Pessoa> ObterCandidatosPorVagaID(int vagaID);

        List<PessoaVagaVM> ObterRankingDeCandidatosPorVagaID(int vagaID);
    }
}