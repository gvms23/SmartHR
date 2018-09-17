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
    [Route("api/v1/pessoas")]
    public class PessoaController : Controller
    {
        private IPessoaService _service;

        public PessoaController(IPessoaService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Inserir([FromBody] Pessoa item)
        {
            try
            {
                _service.Inserir<PessoaValidator>(item);

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
        public IActionResult Atualizar([FromBody] Pessoa item)
        {
            try
            {
                _service.Atualizar<PessoaValidator>(item);
                return new ObjectResult(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete, Route("{id:int}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                _service.Excluir(id);

                return new NoContentResult();
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
                return new ObjectResult(_service.Obter());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet, Route("{pessoaID:int}")]
        public IActionResult Obter(int pessoaID)
        {
            try
            {
                return new ObjectResult(_service.Obter(pessoaID));
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