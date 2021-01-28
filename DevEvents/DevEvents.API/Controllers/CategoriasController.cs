using DevEvents.API.Entidades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEvents.API.Controllers
{
    [Route("api/categorias")]
    public class CategoriasController : ControllerBase
    {
        [HttpGet]
        public IActionResult ObterCategorias()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult ObterCategoria(int id)
        {
            return Ok();
        }

        [HttpPost("{id}")]
        public IActionResult Cadastrar(int id, [FromBody] Categoria categoria)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Categoria categoria)
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
