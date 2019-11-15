using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SharedLibrary.Entities;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEnd.Pages
{
	public class EditModel : PageModel
	{

		public EditModel()
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

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see https://aka.ms/RazorPagesCRUD.
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			var uri = "https://apitarefas.azurewebsites.net/api/Tarefas";

			using (HttpClient client = new HttpClient())
			{
				Tarefa ret = null;
				StringContent cont = new StringContent(JsonConvert.SerializeObject(Tarefa));
				cont.Headers.ContentType.MediaType = "application/json";
				var response = await client.PutAsync(uri + "/" + Tarefa.ID, cont);

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
