using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SharedLibrary.Entities;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEnd.Pages
{
	public class DeleteModel : PageModel
	{

		public DeleteModel()
		{
		}

		[BindProperty]
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

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			var uri = "https://apitarefas.azurewebsites.net/api/Tarefas";
			using (HttpClient client = new HttpClient())
			{
				Tarefa ret = null;
				var response = await client.DeleteAsync(uri + "/" + id);

				if (response.IsSuccessStatusCode)
				{
					var t = await response.Content.ReadAsStringAsync();
					ret = JsonConvert.DeserializeObject<Tarefa>(t);
				}
			}

			return RedirectToPage("./Index");
		}
	}
}
