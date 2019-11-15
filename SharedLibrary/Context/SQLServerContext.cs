using Microsoft.EntityFrameworkCore;
using SharedLibrary.Entities;

namespace SharedLibrary.Context
{
	public partial class SQLServerContext : DbContext
	{
		public SQLServerContext()
		{
		}

		public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options)
		{
		}


		public virtual DbSet<Tarefa> Tasks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Tarefa>(entity =>
			{
				entity.HasKey(e => e.ID)
					.HasName("ID");

				entity.ToTable("Tarefas");

				entity.Property(e => e.ID)
					.HasColumnName("Tarefas_ID")
					.ValueGeneratedOnAdd();

				entity.Property(e => e.Nome)
					.IsRequired()
					.HasColumnName("Nome");

				entity.Property(e => e.Descricao)
					.IsRequired()
					.HasColumnName("Desc");

				entity.Property(e => e.DataEHoraCriacao)
					.IsRequired()
					.HasColumnName("Criacao");

				entity.Property(e => e.DataEHoraEdicao)
					.IsRequired()
					.HasColumnName("Edicao");

				entity.Property(e => e.DataEHoraConclusao)
					.IsRequired()
					.HasColumnName("Conclusao");

				entity.Property(e => e.DataEHoraDelecao)
					.IsRequired()
					.HasColumnName("Delecao");

				entity.Property(e => e.Deleted)
					.IsRequired()
					.HasColumnName("Deletado");
			}
			);


			OnModelCreatingPartial(modelBuilder);
		}


		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
