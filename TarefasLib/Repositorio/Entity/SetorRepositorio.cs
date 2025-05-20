using Microsoft.EntityFrameworkCore;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Repositorio.Entity
{
    public class SetorRepositorio : IRepositorio<Setor>
    {
        private readonly string _connectionString;
        public SetorRepositorio(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void InicializarBancoDados()
        {
            using var context = new AppDbContext(_connectionString);
            context.Database.EnsureCreated();
        }

        public Setor? BuscarPorId(int id)
        {
            using var context = new AppDbContext(_connectionString);
            return context.Setores
                          .Include(s => s.Empresa) // inclui a empresa junto
                          .FirstOrDefault(s => s.Id == id);
        }

        public bool Cadastrar(Setor obj)
        {
            using var context = new AppDbContext(_connectionString);
            context.Setores.Add(obj);
            context.SaveChanges();
            return true;
        }

        public bool Editar(Setor obj)
        {
            using var context = new AppDbContext(_connectionString);
            var setorExistente = context.Setores.Find(obj.Id);
            if (setorExistente != null)
            {
                setorExistente.Nome = obj.Nome;

                context.SaveChanges();
                return true;
            }
            else
            {
                return false;

            }
        }

        public List<Setor> Listar()
        {
            using var context = new AppDbContext(_connectionString);
            return context.Setores.Include(s => s.Empresa).ToList();
        }

        public bool Remover(Setor obj)
        {
            using var context = new AppDbContext(_connectionString);
            var setorExistente = context.Setores.Find(obj.Id);
            if (setorExistente != null)
            {
                context.Setores.Remove(setorExistente);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
