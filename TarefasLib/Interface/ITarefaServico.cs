using TarefasLibrary.Modelo;

namespace TarefasLibrary.Interface
{
    public interface ITarefaServico
    {
        bool Salvar(Tarefa tarefa);
        public Tarefa BuscarPorId(int id);
        public bool Atualizar(Tarefa tarefa, string novostatus, string novadescricao, DateTime novoprazo);
        public List<Tarefa> ListarTodas();



    }
}