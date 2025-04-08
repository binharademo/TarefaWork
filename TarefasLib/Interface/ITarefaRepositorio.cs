using TarefasLibrary.Modelo;

namespace TarefasLibrary.Interface
{
    public interface ITarefaRepositorio
    {
        Tarefa? BuscarPorID(int id);
        List<Tarefa> ListarTodas();
        bool Salvar(Tarefa tarefa);
        public bool Atualizar(Tarefa tarefa, StatusTarefa novostatus, string novadescricao, DateTime novoprazo);

        public bool Atualizar(Tarefa tarefa, StatusTarefa novostatus);
        public List<Tarefa> ListarPorUsuario(int id);
    }
}
