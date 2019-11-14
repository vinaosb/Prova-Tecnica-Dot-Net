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
    public class IndexModel : PageModel
    {
        private readonly SharedLibrary.Context.SQLServerContext _context;

        public IndexModel(SharedLibrary.Context.SQLServerContext context)
        {
            _context = context;
        }

        public IList<Tarefa> Tarefa { get;set; }

        public async Task OnGetAsync()
        {
            Tarefa = await _context.Tasks.ToListAsync();
        }
    }
}
