using TarefasLibrary;
using TarefasLibrary.Interface;

namespace TarefasLibrary.Repositorio
{
    public class TarefaMemoriaRepositorio : ITarefaRepositorio
    {
        public static List<Tarefa> _tarefas = new List<Tarefa>();

        public bool Salvar(Tarefa tarefa)
        {
            _tarefas.Add(tarefa);
            return true;
        }

        public List<Tarefa> ListarTodas()
        {
            return _tarefas;
        }

        public Tarefa? BuscarPorID(int id)
        {
            foreach (var tarefa in _tarefas)
            {
                if (tarefa.Id == id)
                    return tarefa;
            }

            return null;
        }

        public bool Atualizar(Tarefa tarefa, string novostatus, string novadescricao, DateTime novoprazo)
        {
            if(novostatus == "")
            {
                return false;
            }
            tarefa.Status = novostatus;
            tarefa.Descricao = novadescricao;
            tarefa.Prazo = novoprazo;
            return true;
        }
    }
}
