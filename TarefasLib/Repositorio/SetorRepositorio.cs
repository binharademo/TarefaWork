using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Repositorio
{
    public class SetorRepositorio : IRepositorio<Setor>
    {
        private List<Setor> ListaSetores = new();
        private int contadorIDs = 0;

        public Setor? BuscarPorId(int id)
        {
            return ListaSetores.FirstOrDefault(s => s.Id == id);
        }

        public bool Cadastrar(Setor obj)
        {
            obj.Id = ++contadorIDs;
            ListaSetores.Add(obj);
            return true;
        }

        public bool Editar(Setor obj)
        {
            var setorExistente = BuscarPorId(obj.Id);
            if (setorExistente != null)
            {
                setorExistente.Nome = obj.Nome;
                setorExistente.Status = obj.Status;
                return true;
            }
            return false;
        }

        public List<Setor> Listar()
        {
            return ListaSetores;
        }

        public bool Remover(Setor obj)
        {
            return ListaSetores.Remove(obj);
        }
    }

}

