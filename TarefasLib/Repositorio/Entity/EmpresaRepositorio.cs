using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Repositorio.Entity
{
    public class EmpresaRepositorio : IRepositorio<Empresa>
    {
        private readonly string _connectionString;
        public EmpresaRepositorio(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        public void InicializarBancoDados()
        {
            using var context = new AppDbContext(_connectionString);
            context.Database.EnsureCreated();
        }

        public Empresa? BuscarPorId(int id)
        {
            using var context = new AppDbContext(_connectionString);
            return context.Empresas.FirstOrDefault(context => context.Id == id);
        }

        public bool Cadastrar(Empresa obj)
        {
            using var context = new AppDbContext(_connectionString);
            context.Empresas.Add(obj);
            context.SaveChanges();
            return true;
        }

        public bool Editar(Empresa obj)
        {
            using var context = new AppDbContext(_connectionString);
            var empresaExistente = context.Empresas.Find(obj.Id);
            if (empresaExistente != null)
            {
                empresaExistente.Nome = obj.Nome;
                context.SaveChanges();
                return true;
            }

            else
            {
                return false;

            }
        }

        public List<Empresa> Listar()
        {
            using var context = new AppDbContext(_connectionString);
            return context.Empresas.ToList();
        }

    }
}