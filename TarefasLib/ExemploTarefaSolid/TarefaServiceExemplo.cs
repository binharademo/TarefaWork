namespace TarefasLibrary.ExemploTarefaSolid
{
 

    public class TarefaServiceExemplo : ITarefaServiceExemplo
    {
        private readonly ITarefaRepositoryExemplo _tarefaRepository;

        public TarefaServiceExemplo(ITarefaRepositoryExemplo tarefaRepository)
        {
            _tarefaRepository = tarefaRepository ?? throw new ArgumentNullException(nameof(tarefaRepository));
        }

        public TarefaSolidExemplo CriarTarefa(string titulo, string status, string criador, string responsavel,
                                 DateTime prazo, string descricao)
        {
            var tarefa = new TarefaSolidExemplo(0, titulo, status, criador, responsavel, prazo, descricao);
            _tarefaRepository.Adicionar(tarefa);
            return tarefa;
        }

        public bool AtualizarTarefa(TarefaSolidExemplo tarefa)
        {
            if (tarefa == null)
                throw new ArgumentNullException(nameof(tarefa));

            return _tarefaRepository.Atualizar(tarefa);
        }

        public TarefaSolidExemplo BuscarTarefaPorId(int id)
        {
            return _tarefaRepository.BuscarPorId(id);
        }

        public IEnumerable<TarefaSolidExemplo> ListarTodasTarefas()
        {
            return _tarefaRepository.ListarTodas();
        }
    }
}
