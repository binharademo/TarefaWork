using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;
using TarefasLibrary.Repositorio;

namespace TarefasLibrary.Negocio
{
    public class SetorServico
    {
        private readonly SetorRepositorio? _repository;
        public SetorServico(SetorRepositorio repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Setor? BuscarPorId(int id)
        {
            return _repository.BuscarPorId(id);
        }

        public bool Cadastrar(Setor obj)
        {
            return _repository.Cadastrar(obj);
        }

        public bool Editar(int id, string nome, bool status)
        {
            return _repository.Editar(new(id, nome, status));
        }

        public List<Setor> Listar()
        {
            return _repository.Listar();
        }

        public bool Remover(Setor obj)
        {
            return _repository.Remover(obj);
        }
    }
}
