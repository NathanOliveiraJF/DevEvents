﻿using Dapper;
using DevEvents.API.Entidades;
using DevEvents.API.Persistencia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEvents.API.Controllers
{
    [Route("api/eventos")]
    public class EventosController : ControllerBase
    {
        private readonly DevEventsDbContext _dbContext;
        public EventosController(DevEventsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult ObterEventos()
        {
            var eventos = _dbContext
                .Eventos
                .Include(e => e.Categoria)
                .Include(e => e.Usuario)
                .Include(e => e.Inscricoes)
                .ToList();
            return Ok(eventos);
        }

        [HttpGet("{id}")]
        public IActionResult ObterEvento(int id)
        {
            var evento = _dbContext
                .Eventos
                .Include(e => e.Categoria)
                .Include(e => e.Usuario)
                .SingleOrDefault(e => e.Id == id);
            if (evento == null)
                return NotFound();

            return Ok(evento);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] Evento evento)
        {
            _dbContext.Eventos.Add(evento);
            _dbContext.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Evento evento)
        {
            _dbContext.Eventos.Update(evento);
            _dbContext.Entry(evento).Property(e => e.DataCadastro).IsModified = false;
            _dbContext.Entry(evento).Property(e => e.Ativo).IsModified = false;
            _dbContext.Entry(evento).Property(e => e.IdUsuario).IsModified = false;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Cancelar(int id)
        {
            var connectionString = _dbContext.Database.GetDbConnection().ConnectionString;

            using(var sqlConnection = new SqlConnection(connectionString))
            {
                var script = "UPDATE eventos SET ativo = 0 WHERE Id = @id";
                
                sqlConnection.Execute(script, new { id });
            }
            
            return NoContent();
        }

        //api/eventos/1/usuarios/3/inscrever
        [HttpPost("{id}/usuarios/{idUsuario}/inscrever")]
        public IActionResult Inscrever(int id, int idUsuario, [FromBody] Inscricao inscricao)
        {
            var evento = _dbContext.Eventos.FirstOrDefault(e => e.Id == id);
            if(!evento.Ativo)
                return BadRequest();
            
            _dbContext.Inscricoes.Add(inscricao);
            _dbContext.SaveChanges();
            return NoContent();
        }

        [HttpPost("popular")]
        public IActionResult Popular()
        {
            var usuario = new Usuario
            {
                NomeCompleto = "Zezim Dev",
                Email = "zezim@gmail.com"
            };

            var categorias = new List<Categoria>
            {
                new Categoria { Descricao = "C#"},
                new Categoria { Descricao = "Flutter"},
                new Categoria { Descricao = "Xamarin"}
            };

            _dbContext.Usuarios.Add(usuario);
            _dbContext.Categorias.AddRange(categorias);

            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
