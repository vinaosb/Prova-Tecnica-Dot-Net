using System;

namespace SharedLibrary.Entities
{
	public class Tarefa
	{
		public int ID { get; set; }
		public string Nome { get; set; }
		public string Descricao { get; set; }
		public DateTime DataEHoraCriacao { get; set; }
		public DateTime DataEHoraEdicao { get; set; }
		public DateTime DataEHoraConclusao { get; set; }
		public DateTime DataEHoraDelecao { get; set; }
		public bool Status { get; set; }
		public bool Deleted { get; set; }
	}
}
