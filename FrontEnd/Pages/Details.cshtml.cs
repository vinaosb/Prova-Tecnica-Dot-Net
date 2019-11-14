using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Context;
using SharedLibrary.Entities;

namespace FrontEnd.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly SharedLibrary.Context.SQLServerContext _context;

        public DetailsModel(SharedLibrary.Context.SQLServerContext context)
        {
            _context = context;
        }

        public Tarefa Tarefa { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tarefa = await _context.Tasks.FirstOrDefaultAsync(m => m.ID == id);

            if (Tarefa == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
