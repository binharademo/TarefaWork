using TarefasLibrary;
using TarefasLibrary.Interface;

namespace TarefasLibrary.Negocio
{
    public class TarefaServico : ITarefaServico
    {
        ITarefaRepositorio _repositorio;

        public TarefaServico(ITarefaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public bool Salvar(Tarefa tarefa)
        {
            return _repositorio.Salvar(tarefa);
        }

        public Tarefa BuscarPorId(int id)
        {
            return _repositorio.BuscarPorID(id);
        }

        public bool Atualizar(Tarefa tarefa, string novostatus, string novadescricao, DateTime novoprazo)
        {
            return _repositorio.Atualizar(tarefa, novostatus, novadescricao, novoprazo);    
        }

        public List<Tarefa> ListarTodas()
        {
            return _repositorio.ListarTodas();
        }

    }
}
