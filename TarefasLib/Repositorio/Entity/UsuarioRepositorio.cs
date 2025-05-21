using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Repositorio.Entity
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly string _connectionString;
        public UsuarioRepositorio(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void InicializarBancoDados()
        {
            using var context = new AppDbContext(_connectionString);
            context.Database.EnsureCreated();
        }


        public Usuario? BuscarPorId(int id)
        {
            using var context = new AppDbContext(_connectionString);
            return context.Usuarios
                .Include(u => u.SetorUsuario)
                .FirstOrDefault(u => u.Id == id);
        }


        public bool Cadastrar(Usuario obj)
        {
            using var context = new AppDbContext(_connectionString);
            context.Usuarios.Add(obj);
            context.SaveChanges();
            return true;
        }

        public bool Editar(Usuario obj)
        {
            using var context = new AppDbContext(_connectionString);
            var usuarioExistente = context.Usuarios
                .Include(u => u.SetorUsuario)
                .FirstOrDefault(u => u.Id == obj.Id);

            if (usuarioExistente != null)
            {
                usuarioExistente.Nome = obj.Nome;
                usuarioExistente.Senha = obj.Senha;
                usuarioExistente.FuncaoUsuario = obj.FuncaoUsuario;
                usuarioExistente.SetorUsuarioId = obj.SetorUsuarioId;
                context.SaveChanges();
                return true;
            }

            else
            {
                return false;

            }

        }

        public List<Usuario> Listar()
        {
            using var context = new AppDbContext(_connectionString);
            return context.Usuarios
                .Include(u => u.SetorUsuario)
                .ToList();
        }


        public bool Remover(Usuario obj)
        {
           using var context = new AppDbContext(_connectionString);
            var usuarioExistente = context.Usuarios.Find(obj.Id);
            if (usuarioExistente != null)
            {
                context.Usuarios.Remove(usuarioExistente);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Usuario> Listar(Setor SetorUsuario)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> Listar(Usuario.Funcao funcao)
        {
            throw new NotImplementedException();
        }
    }
}
