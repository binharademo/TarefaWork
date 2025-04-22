using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Repositorio.Entity
{
    public class CronometroRepositorio : IRepositorio<Cronometro>
    {
        private readonly string _connectionString;
        public CronometroRepositorio(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        public void InicializarBancoDados()
        {
            using var context = new AppDbContext(_connectionString);
            context.Database.EnsureCreated();
        }

        public Cronometro? BuscarPorId(int id)
        {
            throw new NotImplementedException();

        }

        public bool Cadastrar(Cronometro obj)
        {
            using var context = new AppDbContext(_connectionString);
            context.Cronometros.Add(obj);
            context.SaveChanges();
            return true;
        }

        public bool Editar(Cronometro obj)
        {
            throw new NotImplementedException();
        }

        public List<Cronometro> Listar()
        {
            using var context = new AppDbContext(_connectionString);
            return context.Cronometros.ToList();

        }

        public bool Remover(Cronometro obj)
        {
            throw new NotImplementedException();
        }
    }
}
