using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
			var uri = "https://apitarefas.azurewebsites.net/api/Tarefas";
			using (HttpClient client = new HttpClient())
			{
				Tarefa ret = null;
				var response = await client.GetAsync(uri + "/" + id);

				if (response.IsSuccessStatusCode)
				{
					var t = await response.Content.ReadAsStringAsync();
					ret = JsonConvert.DeserializeObject<Tarefa>(t);
				}
				Tarefa = ret;
			}

            if (Tarefa == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
