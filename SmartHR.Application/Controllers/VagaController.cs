using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHR.Domain.Entities;
using SmartHR.Domain.Interfaces.Services;
using SmartHR.Service.Services;
using SmartHR.Service.Validators;

namespace SmartHR.Application.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/vagas")]
    public class VagaController : Controller
    {
        private IVagaService _service;
        private ICandidaturaService _serviceCandidatura;

        public VagaController(IVagaService service, ICandidaturaService serviceCandidatura)
        {
            _service = service;
            _serviceCandidatura = serviceCandidatura;
        }

        [HttpPost]
        public IActionResult Inserir([FromBody] Vaga item)
        {
            try
            {
                _service.Inserir<VagaValidator>(item);

                return new ObjectResult(item.Id);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody] Vaga item)
        {
            try
            {
                _service.Atualizar<VagaValidator>(item);
                return new ObjectResult(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete, Route("{vagaID:int}")]
        public IActionResult Excluir(int vagaID)
        {
            try
            {
                _service.Excluir(vagaID);

                return new ObjectResult("Registro excluído com sucesso.");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public IActionResult Obter()
        {
            try
            {
                return new ObjectResult(_service.ObterTodos());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet, Route("{vagaID:int}")]
        public IActionResult ObterPorID(int vagaID)
        {
            try
            {
                return new ObjectResult(_service.ObterPorID(vagaID));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{vagaID:int}/candidaturas")]
        public IActionResult ObterCandidaturasPorVagaID(int vagaID)
        {
            try
            {
                return new ObjectResult(_serviceCandidatura.ObterCandidatosPorVagaID(vagaID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{vagaID:int}/candidaturas/ranking")]
        public IActionResult ObterRankingDeCandidaturasPorVagaID(int vagaID)
        {
            try
            {
                return new ObjectResult(_serviceCandidatura.ObterRankingDeCandidatosPorVagaID(vagaID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}