using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Repositorio.Entity
{
    public class ComentarioRepositorio : IComentarioRepositorio
    {
        private readonly string _connectionString;
        public ComentarioRepositorio(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        public void InicializarBancoDados()
        {
            using var context = new AppDbContext(_connectionString);
            context.Database.EnsureCreated();
        }

        public Comentario? BuscarPorId(int id)
        {
            using var context = new AppDbContext(_connectionString);
            return context.Comentarios.FirstOrDefault(context => context.Id == id);
        }

        public bool Cadastrar(Comentario obj)
        {
            using var context = new AppDbContext(_connectionString);
            context.Comentarios.Add(obj);
            context.SaveChanges();
            return true;
        }

        public bool Editar(Comentario obj)
        {
            using var context = new AppDbContext(_connectionString);
            var comentarioExistente = context.Comentarios.Find(obj.Id);
            if (comentarioExistente != null)
            {
                comentarioExistente.Descricao = obj.Descricao;
                    context.SaveChanges();
                return true;
            }

            else
            {
                return false;

            }
        }

        public List<Comentario> Listar()
        {
            using var context = new AppDbContext(_connectionString);
            return context.Comentarios.ToList();
        }

        public bool Remover(Comentario obj)
        {
            using var context = new AppDbContext(_connectionString);
            var comentarioExistente = context.Comentarios.Find(obj.Id);
            if (comentarioExistente != null)
            {
                context.Comentarios.Remove(comentarioExistente);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Comentario> BuscarPorTarefa(int id)
        {
            using var context = new AppDbContext(_connectionString);
            return context.Comentarios.Where(context => context.TarefaId == id).ToList();
        }
    }
}
