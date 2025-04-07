namespace TarefasLibrary.ExemploTarefaSolid
{
    public interface ITarefaRepositoryExemplo
    {
        void Adicionar(TarefaSolidExemplo tarefa);

        bool Atualizar(TarefaSolidExemplo tarefa);

        TarefaSolidExemplo BuscarPorId(int id);

        IEnumerable<TarefaSolidExemplo> ListarTodas();
    }
}
