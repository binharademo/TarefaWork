using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Repositorio.Entity
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Cronometro> Cronometros { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

        public DbSet<Empresa> Empresas { get; set; }

        public DbSet<Setor> Setores { get; set; }

        private readonly string _connectionString;

        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarefa>(entity =>
            {
                entity.ToTable("Tarefas"); // Nome da tabela no banco de dados
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Id)
                      .ValueGeneratedOnAdd(); // autoincremento  
                entity.Property(t => t.Titulo)
                      .IsRequired();
                entity.Property(t => t.Descricao);
                entity.Property(t => t.StatusTarefa)
                      .HasConversion<string>(); // Armazena como texto (opcional)  
                entity.Property(t => t.PrioridadeTarefa)
                      .HasConversion<string>(); // Armazena como texto (opcional)  
                entity.Property(t => t.DataCriacao)
                      .IsRequired();
                entity.Property(t => t.Prazo)
                      .IsRequired();

                entity.HasOne(t => t.Criador).WithMany(u => u.TarefasDono);
                entity.HasOne(t => t.Responsavel).WithMany(u => u.TarefasResponsavel);
                entity.HasMany(t => t.Membros).WithMany(u => u.TarefasMembro);
                entity.HasMany(t => t.Tempos);
                entity.HasMany(t => t.listaComentarios);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios"); // Nome da tabela no banco de dados
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Id)
                      .ValueGeneratedOnAdd(); // autoincremento  

                entity.Property(u => u.Nome)
                      .IsRequired();

                entity.Property(u => u.Senha);

                entity.Property(u => u.FuncaoUsuario)
                      .HasConversion<string>(); // Armazena como texto (opcional)  

                entity.Property(u => u.SetorUsuario)
                      .HasConversion<string>(); // Armazena como texto (opcional)  

                entity.HasMany(t => t.listaComentarios);
            });

            modelBuilder.Entity<Cronometro>(entity =>
            {
                entity.ToTable("Cronometros"); // Nome da tabela no banco de dados
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Id)
                      .ValueGeneratedOnAdd(); // autoincremento  
                entity.Property(c => c.Inicio)
                      .IsRequired();
                entity.Property(c => c.Fim);
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.ToTable("Comentarios"); // Nome da tabela no banco de dados
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id)
                      .ValueGeneratedOnAdd(); // autoincremento  
                entity.Property(c => c.Descricao)
                      .IsRequired();
                entity.Property(c => c.DataCriacao)
                      .IsRequired();
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.ToTable("Empresas"); // Nome da tabela no banco de dados
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd(); // autoincremento  
                entity.Property(e => e.Nome)
                        .IsRequired();
                entity.Property(e => e.Cnpj)
                      .IsRequired();
                entity.HasMany(e => e.Setores);
            });

            modelBuilder.Entity<Setor>(entity =>
            {
                entity.ToTable("Setores"); // Nome da tabela no banco de dados
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Id)
                      .ValueGeneratedOnAdd(); // autoincremento
                entity.Property(s => s.Nome)
                      .IsRequired();
                entity.Property(s => s.Status)
                      .IsRequired();

            });

        }
    }
}

