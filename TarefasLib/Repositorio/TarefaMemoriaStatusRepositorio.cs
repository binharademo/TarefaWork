using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Repositorio
{
    public class TarefaMemoriaStatusRepositorio : IRepositorio<Tarefa>
    {
        private List<Tarefa> _tarefas = new List<Tarefa>();



        private int GeraNovoId()
        {
            if (_tarefas.Count == 0) return 1;
            return _tarefas.Max(t => t.Id) + 1;
        }





        /// ////////////////////////////////////////////////////////////////////////////////


        public Tarefa? BuscarPorId(int id)
        {
            foreach (var tarefa in _tarefas)
            {
                if (tarefa.Id == id)
                    return tarefa;
            }

            return null;
        }

        public bool Cadastrar(Tarefa obj)
        {
                obj.Id = GeraNovoId();
                _tarefas.Add(obj);
                return true;
        }

        public List<Tarefa> Listar()
        {
            return _tarefas;
        }

        public bool Editar(Tarefa obj, Tarefa.Status novostatus)
        {
            if (novostatus == null)
            {
                return false;
            }
            obj.StatusTarefa = novostatus;
            return true;
        }


        public bool Editar(Tarefa obj, Tarefa.Status novostatus, string novadescricao, DateTime novoprazo)
        {
            if (novostatus == null)
            {
                return false;
            }
            obj.StatusTarefa = novostatus;
            obj.Descricao = novadescricao;
            obj.Prazo = novoprazo;
            return true;
        }


        public bool Remover(Tarefa obj)
        {
            throw new NotImplementedException("Teste");
        }

        public bool Editar(Tarefa obj)
        {
            throw new NotImplementedException("TesteEditar");
        }

        public List<Tarefa> Listar(Enum obj)
        {
            throw new NotImplementedException();
        }
    }

}
