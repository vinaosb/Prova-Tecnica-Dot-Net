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

			_context.Entry(tarefa).Entity.DataEHoraEdicao = DateTime.Now;
			if (tarefa.Status)
				_context.Entry(tarefa).Entity.DataEHoraConclusao = DateTime.Now;

			_context.Entry(tarefa).State = EntityState.Modified;

			try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarefaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tarefas
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Tarefa>> PostTarefa(Tarefa tarefa)
        {
            _context.Tasks.Add(tarefa);
            await _context.SaveChangesAsync();
			tarefa.DataEHoraCriacao = DateTime.Now;
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

            _context.Tasks.Remove(tarefa);
            await _context.SaveChangesAsync();

            return tarefa;
        }

        private bool TarefaExists(int id)
        {
            return _context.Tasks.Any(e => e.ID == id);
        }
    }
}
