using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SharedLibrary.Context;
using SharedLibrary.Entities;

namespace FrontEnd.Pages
{
    public class CreateModel : PageModel
    {
        private readonly SharedLibrary.Context.SQLServerContext _context;

        public CreateModel(SharedLibrary.Context.SQLServerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Tarefa Tarefa { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

			var request = new HttpRequestMessage(HttpMethod.Post, "https://api.github.com/repos/aspnet/AspNetCore.Docs/branches");
            _context.Tasks.Add(Tarefa);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
