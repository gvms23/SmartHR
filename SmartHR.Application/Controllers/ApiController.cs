using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHR.Domain.Entities;
using SmartHR.Domain.Interfaces.Services;
using SmartHR.Domain.ViewModels;
using SmartHR.Service.Validators;

namespace SmartHR.Application.Controllers
{
    [Route("~/")]
    public class ApiController : Controller
    {

        private IVagaService _serviceVaga;
        private IPessoaService _servicePessoa;
        private ICandidaturaService _serviceCandidatura;

        public ApiController(
            IVagaService serviceVaga,
            IPessoaService servicePessoa,
            ICandidaturaService serviceCandidatura)
        {
            _serviceVaga = serviceVaga;
            _servicePessoa = servicePessoa;
            _serviceCandidatura = serviceCandidatura;
        }

        public IActionResult Get()
        {
            try
            {
                var vaga = _serviceVaga.Inserir<VagaValidator>(new Vaga
                {
                    Ativo = true,
                    Empresa = "Teste",
                    Titulo = "Vaga Teste",
                    Descricao = "Criar os mais diferentes tipos de teste",
                    Localizacao = "A",
                    Nivel = 3
                });

                var pessoas = _servicePessoa.InserirRange<PessoaValidator>(new List<Pessoa>{
                new Pessoa
                {
                    Nome = "Gabriel Vicente",
                    Profissao = "Engenheiro de Software SR",
                    Localizacao = "C",
                    Nivel = 4
                },
                new Pessoa
                {
                    Nome = "João",
                    Profissao = "Engenheiro de Software PL",
                    Localizacao = "B",
                    Nivel = 3
                },
                new Pessoa
                {
                    Nome = "Gabriel Vicente",
                    Profissao = "Engenheiro de Software JR",
                    Localizacao = "A",
                    Nivel = 2
                }
                });

                var candidatura = _serviceCandidatura.ConverterInserir(new CandidaturaVM
                {
                    id_pessoa = 1,
                    id_vaga = 1
                });
                var candidatura2 = _serviceCandidatura.ConverterInserir(new CandidaturaVM
                {
                    id_pessoa = 2,
                    id_vaga = 1
                });
                var candidatura3 = _serviceCandidatura.ConverterInserir(new CandidaturaVM
                {
                    id_pessoa = 3,
                    id_vaga = 1
                });

                return new ObjectResult(new
                {
                    Retorno = "API .NET Core da aplicação SmartHR está sendo executado! Dados de teste já inseridos.",
                    Vagas = vaga,
                    Candidaturas = candidatura,
                    Pessoas = pessoas
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}