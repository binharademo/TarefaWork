using TarefasLibrary.Modelo;

namespace TarefasLibrary.Interface
{
    public interface ITarefaServico
    {
        bool Salvar(Tarefa tarefa);
        Tarefa? BuscarPorId(int id);
        bool Atualizar(Tarefa tarefa, StatusTarefa novostatus, string novadescricao, DateTime novoprazo);
        List<Tarefa> ListarTodas();
        List<Tarefa> ListarPorUsuario(int id);

    }
}