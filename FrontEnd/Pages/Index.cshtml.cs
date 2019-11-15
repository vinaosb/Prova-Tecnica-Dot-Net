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
			var uri = "https://apitarefas.azurewebsites.net/api/Tarefas";
			using (HttpClient client = new HttpClient())
			{
				IEnumerable<Tarefa> ret = null;
				var response = await client.GetAsync(uri);

				if (response.IsSuccessStatusCode)
				{
					var t = await response.Content.ReadAsStringAsync();
					ret = JsonConvert.DeserializeObject<IEnumerable<Tarefa>>(t);
				}
				Tarefa =  ret.ToList();
			}
		}
    }
}
