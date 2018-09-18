using System;
using System.Collections.Generic;
using System.Linq;
using SmartHR.Domain.Entities;
using SmartHR.Domain.Interfaces.Repositories;
using SmartHR.Domain.Interfaces.Services;
using SmartHR.Domain.ViewModels;
using SmartHR.Service.Validators;

namespace SmartHR.Service.Services
{
    public class CandidaturaService : ServiceBase<Candidatura>, ICandidaturaService
    {
        private ICandidaturaRepository _repository;

        private IVagaService _serviceVaga;

        private IPessoaService _servicePessoa;

        public CandidaturaService(
                ICandidaturaRepository repository,
                IVagaService serviceVaga,
                IPessoaService servicePessoa)
            : base(repository)
        {
            _repository = repository;
            _serviceVaga = serviceVaga;
            _servicePessoa = servicePessoa;
        }


        public List<Candidatura> ObterTodos()
        {
            return _repository.ObterTodos(null, i => i.Pessoa, i => i.Vaga).ToList();
        }

        public bool Excluir(int pessoaID, int vagaID)
        {
            return _repository.Excluir(c => c.PessoaID == pessoaID && c.VagaID == vagaID);
        }

        public List<Candidatura> ObterPorVagaID(int vagaID)
        {
            return _repository.ObterTodos(x => x.VagaID == vagaID, i => i.Pessoa, i => i.Vaga).ToList();
        }

        public List<Pessoa> ObterCandidatosPorVagaID(int vagaID)
        {
            return ObterPorVagaID(vagaID)
                            .Select(x => x.Pessoa)
                            .ToList();
        }

        public List<PessoaVagaVM> ObterRankingDeCandidatosPorVagaID(int vagaID)
        {
            return ObterPorVagaID(vagaID)
                             .Select(c => new PessoaVagaVM(c))
                             .OrderByDescending(o => o.Score)
                             .ToList();
        }

        public Candidatura ConverterInserir(CandidaturaVM candidaturaVM)
        {
            try
            {
                var candidatura = new Candidatura(candidaturaVM);

                var candidato = _servicePessoa.Obter(candidatura.PessoaID);

                var vaga = _serviceVaga.Obter(candidatura.VagaID);

                if (candidato == null)
                    throw new Exception($"Candidato não encontrado com o ID: {candidatura.PessoaID}");

                if (vaga == null)
                    throw new Exception($"Vaga não encontrada com o ID: {candidatura.VagaID}");

                candidatura.Score = CalcularScoreCandidato(vaga, candidato);

                return Inserir<CandidaturaValidator>(candidatura);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private int CalcularSomaDistancia(string localidadeEmpresa, string localidadeCandidato)
        {
            //Quanto maior a soma, mais longe está o candidato.
            var somaLocalizacao = 0;

            //Regra geral (será usado também invertidamente, ex: AB = 5, BA = 5).
            const int AB = 5;
            const int BC = 7;
            const int BD = 3;
            const int CE = 4;
            const int DE = 10;
            const int DF = 8;

            switch (localidadeEmpresa)
            {

                case "A":
                    switch (localidadeCandidato)
                    {
                        case "A":
                            somaLocalizacao += 0;
                            break;
                        case "B":
                            somaLocalizacao += AB;
                            break;
                        case "C":
                            somaLocalizacao += (AB + BC);
                            break;
                        case "D":
                            somaLocalizacao += (AB + BD);
                            break;
                        case "E":
                            somaLocalizacao += (AB + BC + CE);
                            break;
                        case "F":
                            somaLocalizacao += (AB + BD + DF);
                            break;
                    }
                    break;

                case "B":
                    switch (localidadeCandidato)
                    {
                        case "A":
                            somaLocalizacao += AB;
                            break;
                        case "B":
                            somaLocalizacao += 0;
                            break;
                        case "C":
                            somaLocalizacao += BC;
                            break;
                        case "D":
                            somaLocalizacao += BD;
                            break;
                        case "E":
                            somaLocalizacao += (BC + CE);
                            break;
                        case "F":
                            somaLocalizacao += (BD + DF);
                            break;
                    }
                    break;

                case "C":
                    switch (localidadeCandidato)
                    {
                        case "A":
                            somaLocalizacao += (AB + BC);
                            break;
                        case "B":
                            somaLocalizacao += BC;
                            break;
                        case "C":
                            somaLocalizacao += 0;
                            break;
                        case "D":
                            somaLocalizacao += (BD + BC);
                            break;
                        case "E":
                            somaLocalizacao += CE;
                            break;
                        case "F":
                            somaLocalizacao += (DF + BD + BC);
                            break;
                    }
                    break;

                case "D":
                    switch (localidadeCandidato)
                    {
                        case "A":
                            somaLocalizacao += (AB + BD);
                            break;
                        case "B":
                            somaLocalizacao += BD;
                            break;
                        case "C":
                            somaLocalizacao += (BC + BD);
                            break;
                        case "D":
                            somaLocalizacao += 0;
                            break;
                        case "E":
                            somaLocalizacao += DE;
                            break;
                        case "F":
                            somaLocalizacao += DF;
                            break;
                    }
                    break;

                case "E":
                    switch (localidadeCandidato)
                    {
                        case "A":
                            somaLocalizacao += (AB + BC + CE);
                            break;
                        case "B":
                            somaLocalizacao += (BC + CE);
                            break;
                        case "C":
                            somaLocalizacao += CE;
                            break;
                        case "D":
                            somaLocalizacao += DE;
                            break;
                        case "E":
                            somaLocalizacao += 0;
                            break;
                        case "F":
                            somaLocalizacao += (DF + DE);
                            break;
                    }
                    break;

                case "F":
                    switch (localidadeCandidato)
                    {
                        case "A":
                            somaLocalizacao += (AB + BD + DF);
                            break;
                        case "B":
                            somaLocalizacao += (BD + DF);
                            break;
                        case "C":
                            somaLocalizacao += (BC + BD + DF);
                            break;
                        case "D":
                            somaLocalizacao += DF;
                            break;
                        case "E":
                            somaLocalizacao += (DE + DF);
                            break;
                        case "F":
                            somaLocalizacao += 0;
                            break;
                    }
                    break;

            }

            return somaLocalizacao;

        }

        private int CalcularValorDistancia(int distancia)
        {

            if (distancia >= 0 && distancia <= 5)
                return 100;

            else if (distancia > 5 && distancia <= 10)
                return 75;

            else if (distancia > 10 && distancia <= 15)
                return 50;

            else if (distancia > 15 && distancia <= 20)
                return 25;

            else if (distancia > 20)
                return 0;

            else
                throw new InvalidOperationException("Valor não esperado");
        }


        private int CalcularScoreCandidato(Vaga vaga, Pessoa candidato)
        {

            /*
                SCORE = (N+D)/2
             */

            //Passo 1 - Calcular N ->  n = 100 - (25 * |experienciaVaga = experienciaCandidato|)
            var valorN = 100 - (25 * vaga.Nivel - candidato.Nivel);

            //Com base no diagrama com letras, obtem a soma das localizações
            var distancia = CalcularSomaDistancia(vaga.Localizacao, candidato.Localizacao);

            /* Passo 3 - Calcular D -> Menor distância entre o candidato e a vaga
                >=0 <= 5: 100
                >5 <= 10: 75
                >10 <= 15: 50
                >15 <= 20: 25
                >20: 0
            */
            var valorD = CalcularValorDistancia(distancia);

            return (valorN + valorD) / 2;
        }
    }
}