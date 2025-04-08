using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

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

        public Tarefa? BuscarPorId(int id)
        {
            return _repositorio.BuscarPorID(id);
        }

        public bool Atualizar(Tarefa tarefa, StatusTarefa novostatus, string novadescricao, DateTime novoprazo)
        {
            return _repositorio.Atualizar(tarefa, novostatus, novadescricao, novoprazo);    
        }

        public bool Atualizar(Tarefa tarefa, StatusTarefa novostatus)
        {
            return _repositorio.Atualizar(tarefa, novostatus);
        }

        public List<Tarefa> ListarTodas()
        {
            return _repositorio.ListarTodas();
        }

        public List<Tarefa> ListarPorUsuario(int id)
        {
            return _repositorio.ListarPorUsuario(id);
        }
    }
}
