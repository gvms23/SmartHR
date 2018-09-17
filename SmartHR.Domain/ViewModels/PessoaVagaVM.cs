using SmartHR.Domain.Entities;

namespace SmartHR.Domain.ViewModels
{
    public class PessoaVagaVM : Pessoa
    {
        public PessoaVagaVM(Candidatura candidatura)
        {
            Nome = candidatura.Pessoa.Nome;
            Profissao = candidatura.Pessoa.Nome;
            Localizacao = candidatura.Pessoa.Localizacao;
            Nivel = candidatura.Pessoa.Nivel;
            Score = candidatura.Score;
        }
        public int Score { get; set; }
    }
}