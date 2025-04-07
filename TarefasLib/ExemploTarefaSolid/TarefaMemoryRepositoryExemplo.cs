namespace TarefasLibrary.ExemploTarefaSolid
{


    public class TarefaMemoryRepositoryExemplo : ITarefaRepositoryExemplo
    {
        private readonly List<TarefaSolidExemplo> _tarefas = new List<TarefaSolidExemplo>();

        public void Adicionar(TarefaSolidExemplo tarefa)
        {
            if (tarefa == null)
                throw new ArgumentNullException(nameof(tarefa));

            if (_tarefas.Any(t => t.Id == tarefa.Id && tarefa.Id != 0))
                throw new InvalidOperationException($"Já existe uma tarefa com o ID {tarefa.Id}");

            if (tarefa.Id == 0)
            {
                tarefa.Id = GerarNovoId();
            }

            _tarefas.Add(tarefa);
        }



        public bool Atualizar(TarefaSolidExemplo tarefa)
        {
            if (tarefa == null)
                throw new ArgumentNullException(nameof(tarefa));

            var tarefaExistente = _tarefas.FirstOrDefault(t => t.Id == tarefa.Id);
            if (tarefaExistente == null)
                return false;

            tarefaExistente.Titulo = tarefa.Titulo;
            tarefaExistente.Status = tarefa.Status;
            tarefaExistente.Criador = tarefa.Criador;
            tarefaExistente.Responsavel = tarefa.Responsavel;
            tarefaExistente.Prazo = tarefa.Prazo;
            tarefaExistente.Descricao = tarefa.Descricao;

            return true;
        }

        public TarefaSolidExemplo BuscarPorId(int id)
        {
            return _tarefas.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<TarefaSolidExemplo> ListarTodas()
        {
            return _tarefas.ToList();
        }

        private int GerarNovoId()
        {
            return _tarefas.Count == 0 ? 1 : _tarefas.Max(t => t.Id) + 1;
        }

    }
}
