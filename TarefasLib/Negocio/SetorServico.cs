using System.Collections;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;
using TarefasLibrary.Repositorio;

namespace TarefasLibrary.Negocio
{
    public class SetorServico
    {
        private readonly IRepositorio<Setor> _repository;
        public SetorServico(IRepositorio<Setor> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public bool Cadastrar(Setor setor)
        {
            return _repository.Cadastrar(setor);
        }

        public bool Editar(int id, string nome, bool status)
        {
            Setor s = _repository.BuscarPorId(id);
            if (s == null)
                return false;

            s.Nome = nome;
            s.Status = status;

            return _repository.Editar(s);
        }

        public bool Editar(Setor setor)
        {
            return _repository.Editar(setor);
        }

        public List<Setor> Listar()
        {
            return _repository.Listar();
        }

        public bool Remover(Setor setor)
        {
            return _repository.Remover(setor);
        }

        public Setor BuscarPorId(int id)
        {
            return _repository.BuscarPorId(id);
        }
    }
}