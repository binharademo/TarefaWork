
using TarefasLibrary;

namespace TarefasLibrary.Interface
{
    public interface ITarefaRepositorio
    {
        Tarefa? BuscarPorID(int id);
        List<Tarefa> ListarTodas();
        bool Salvar(Tarefa tarefa);
        public bool Atualizar(Tarefa tarefa, string novostatus, string novadescricao, DateTime novoprazo);
        public List<Tarefa> ListarPorUsuario(int id);
    }
}