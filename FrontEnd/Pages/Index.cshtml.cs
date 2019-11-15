using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SharedLibrary.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEnd.Pages
{
	public class IndexModel : PageModel
	{

		public IndexModel()
		{
		}

		public IList<Tarefa> Tarefa { get; set; }

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
				Tarefa = ret.ToList();
			}
		}
	}
}
