using TarefasLibrary.Modelo;

namespace TarefasLibrary.Interface
{
    public interface ITarefaServico
    {
        bool Salvar(Tarefa tarefa);
        public Tarefa? BuscarPorId(int id);
        public bool Atualizar(Tarefa tarefa, Tarefa.Status status, string novadescricao, DateTime novoprazo, string novotitulo, Tarefa.Prioridade prioridade);
        public List<Tarefa> ListarTodas();
        public List<Tarefa> ListarPorUsuario(int id);

    }
}