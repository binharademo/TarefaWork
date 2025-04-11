using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Negocio
{
    public class TarefaServico : ITarefaServico, ICronometroServico<Tarefa>
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

        public bool Atualizar(Tarefa tarefa, string novostatus, string novadescricao, DateTime novoprazo)
        {
            return _repositorio.Atualizar(tarefa, novostatus, novadescricao, novoprazo);    
        }

        public List<Tarefa> ListarTodas()
        {
            return _repositorio.ListarTodas();
        }

        public List<Tarefa> ListarPorUsuario(int id)
        {
            return _repositorio.ListarPorUsuario(id);
        }

        public bool MarcarMembro(Tarefa tarefa, Usuario membro)
        {
            var tarefaPesquisa = BuscarPorId(tarefa.Id);
            if (tarefaPesquisa == null)
                return false;

            if (tarefaPesquisa.Membros.Contains(membro))
                return false;

            return _repositorio.MarcarMembro(tarefa, membro);
        }

        public TimeSpan PausaCronometro(Tarefa tarefa)
        {
            if (tarefa.Tempos.Count == 0) 
                return TimeSpan.Zero;

            if (!tarefa.Tempos.Last().EmAndamento())
                return tarefa.TempoTotal;

            tarefa.Tempos.Last().Stop();
            tarefa.TempoTotal = tarefa.Tempos.Select(t => t.Total)
                 .Aggregate(TimeSpan.Zero, (acc, curr) => acc + curr);

            return tarefa.TempoTotal;
        }

        public bool IniciaCronometro(Tarefa tarefa)
        {
            if (tarefa.Tempos.Count > 0 && tarefa.Tempos.Last().EmAndamento())
                return false;

            tarefa.Tempos.Add(new Cronometro());
            return true;
        }

    }
}
