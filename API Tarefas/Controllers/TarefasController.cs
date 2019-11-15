using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Context;
using SharedLibrary.Entities;

namespace Prova_Tecnica_Dot_Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly SQLServerContext _context;

        public TarefasController(SQLServerContext context)
        {
            _context = context;
        }

        // GET: api/Tarefas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/Tarefas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> GetTarefa(int id)
        {
            var tarefa = await _context.Tasks.FindAsync(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            return tarefa;
        }

        // PUT: api/Tarefas/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarefa(int id, Tarefa tarefa)
		{
			if (id != tarefa.ID)
            {
                return BadRequest();
			}
			var tar = await _context.Tasks.FindAsync(id);

			if (tarefa.Nome == null)
				_context.Entry(tarefa).Entity.Nome = tar.Nome;
			if (tarefa.Descricao == null)
				_context.Entry(tarefa).Entity.Descricao = tar.Descricao;

			_context.Entry(tarefa).Entity.DataEHoraCriacao = tar.DataEHoraCriacao;
			_context.Entry(tarefa).Entity.DataEHoraDelecao = tar.DataEHoraDelecao;
			_context.Entry(tarefa).Entity.DataEHoraEdicao = DateTime.Now;
			if (tarefa.Status)
				_context.Entry(tarefa).Entity.DataEHoraConclusao = DateTime.Now;
			else
				_context.Entry(tarefa).Entity.DataEHoraConclusao = tar.DataEHoraConclusao;

			_context.Entry(tarefa).State = EntityState.Modified;

			await _context.SaveChangesAsync();

			return NoContent();
        }

        // POST: api/Tarefas
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Tarefa>> PostTarefa(Tarefa tarefa)
		{
			tarefa.DataEHoraCriacao = DateTime.Now;
			_context.Tasks.Add(tarefa);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTarefa", new { id = tarefa.ID }, tarefa);
        }

		/// <summary>
		/// Deletes a specific Tarefa.
		/// </summary>
		/// <param name="id"></param>   
		[HttpDelete("{id}")]
        public async Task<ActionResult<Tarefa>> DeleteTarefa(int id)
        {
            var tarefa = await _context.Tasks.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
			_context.Entry(tarefa).Entity.Deleted = true;
			_context.Entry(tarefa).Entity.DataEHoraDelecao = DateTime.Now;

			_context.Entry(tarefa).State = EntityState.Modified;

			await _context.SaveChangesAsync();

            return tarefa;
        }

        private bool TarefaExists(int id)
        {
            return _context.Tasks.Any(e => e.ID == id);
        }
    }
}
