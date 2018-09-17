using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHR.Domain.Entities;
using SmartHR.Domain.Interfaces.Services;
using SmartHR.Domain.ViewModels;
using SmartHR.Service.Services;
using SmartHR.Service.Validators;

namespace SmartHR.Application.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/candidaturas")]
    public class CandidaturaController : Controller
    {
        private ICandidaturaService _service;

        public CandidaturaController(ICandidaturaService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Inserir([FromBody] CandidaturaVM item)
        {
            try
            {
                return new ObjectResult(_service.ConverterInserir(item));
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
        [HttpDelete, Route("{pessoaID:int}/{vagaID:int}")]
        public IActionResult Excluir(int pessoaID, int vagaID)
        {
            try
            {
                return new ObjectResult(_service.Excluir(pessoaID, vagaID));
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

        [HttpGet, Route("{candidaturaID:int}")]
        public IActionResult Obter(int candidaturaID)
        {
            try
            {
                return new ObjectResult(_service.Obter(candidaturaID));
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
    }
}