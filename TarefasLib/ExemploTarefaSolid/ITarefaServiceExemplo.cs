namespace TarefasLibrary.ExemploTarefaSolid
{
   
    public interface ITarefaServiceExemplo
    {
        TarefaSolidExemplo CriarTarefa(string titulo, string status, string criador, string responsavel,
                          DateTime prazo, string descricao);

        bool AtualizarTarefa(TarefaSolidExemplo tarefa);

        TarefaSolidExemplo BuscarTarefaPorId(int id);

        IEnumerable<TarefaSolidExemplo> ListarTodasTarefas();
    }
}
