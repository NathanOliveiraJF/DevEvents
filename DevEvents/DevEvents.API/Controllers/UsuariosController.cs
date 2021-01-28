using DevEvents.API.Entidades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEvents.API.Controllers
{
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        [HttpGet]
        public IActionResult ObterUsuarios()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult ObterUsuario(int id)
        {
            return Ok();
        }

        [HttpPost("{id}")]
        public IActionResult Cadastrar(int id, [FromBody] Usuario usuario)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Usuario usuario)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            return NoContent();
        }

    }
}
