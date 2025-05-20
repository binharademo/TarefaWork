using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Repositorio.Entity
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly string _connectionString;
        public TarefaRepositorio(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void InicializarBancoDados()
        {
            using var context = new AppDbContext(_connectionString);
            context.Database.EnsureCreated();
        }
        public bool Atualizar(Tarefa tarefa, Tarefa.Status novostatus, string novadescricao, DateTime novoprazo, string novotitulo, Tarefa.Prioridade novaprioridade)
        {
            using var context = new AppDbContext(_connectionString);
            var tarefaExistente = context.Tarefas.Find(tarefa.Id);
            if (tarefaExistente != null)
            {
                tarefaExistente.Titulo = novotitulo;
                tarefaExistente.PrioridadeTarefa = novaprioridade;
                tarefaExistente.StatusTarefa = novostatus;
                tarefaExistente.Descricao = novadescricao;
                tarefaExistente.Prazo = novoprazo;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Atualizar(Tarefa tarefa, Tarefa.Status novostatus)
        {
            using var context = new AppDbContext(_connectionString);
            var tarefaExistente = context.Tarefas.Find(tarefa.Id);
            if (tarefaExistente != null)
            {
                tarefaExistente.StatusTarefa = novostatus;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Tarefa? BuscarPorID(int id)
        {
            using var context = new AppDbContext(_connectionString);
            return context.Tarefas.Find(id);
        }

        public List<Tarefa> ListarPorUsuario(int id)
        {
            using var context = new AppDbContext(_connectionString);
            return context.Tarefas.Where(t => t.Responsavel.Id == id || t.Criador.Id == id).ToList();
        }

        public List<Tarefa> ListarTodas()
        {
            using var context = new AppDbContext(_connectionString);
            return context.Tarefas.ToList();
        }

        public bool MarcarMembro(Tarefa tarefa, Usuario membro)
        {
            using var context = new AppDbContext(_connectionString);
            var tarefaExistente = context.Tarefas.Find(tarefa.Id);
            if (tarefaExistente == null)
                return false;
            
            tarefaExistente.Membros.Add(membro);
            context.SaveChanges();
            return true;
        }

        public bool Salvar(Tarefa tarefa)
        {
            using var context = new AppDbContext(_connectionString);
            context.Tarefas.Add(tarefa);
            return context.SaveChanges() > 0;
        }

        public List<Tarefa> Buscar(FiltroTarefa filtro)
        {
            using var context = new AppDbContext(_connectionString);
            return context.Tarefas.Where(t =>
                   (string.IsNullOrEmpty(filtro.Nome) ? true : t.Titulo.Contains(filtro.Nome))
                && (filtro.Prioridade == null || t.PrioridadeTarefa == filtro.Prioridade)
                && (filtro.Status == null || t.StatusTarefa == filtro.Status)
                && (filtro.Criador == null || t.Criador.Id == filtro.Criador)
                && (filtro.Responsavel == null || t.Responsavel.Id == filtro.Responsavel)
                && (filtro.Membro == null || t.Membros.Exists(m => m.Id == filtro.Membro))
                && (filtro.Inicio == null || t.DataCriacao >= filtro.Inicio)
                && (filtro.Fim == null || t.DataCriacao <= filtro.Fim)
            ).ToList();
        }
    }
}
