using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Repositorio
{
    public class TarefaMemoriaRepositorio : ITarefaRepositorio
    {
        private List<Tarefa> _tarefas = new List<Tarefa>();

        public bool Salvar(Tarefa tarefa)
        {
            
            tarefa.Id = GeraNovoId();
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

        public bool Atualizar(Tarefa tarefa, StatusTarefa novostatus, string novadescricao, DateTime novoprazo)
        {
            if(novostatus == null)
            {
                return false;
            }
            tarefa.Status = novostatus;
            tarefa.Descricao = novadescricao;
            tarefa.Prazo = novoprazo;
            return true;
        }

        public bool Atualizar(Tarefa tarefa, StatusTarefa novostatus)
        {
            if (novostatus == null)
            {
                return false;
            }
            tarefa.Status = novostatus;
            return true;
        }

        private int GeraNovoId()
        {
            if (_tarefas.Count == 0) return 1;
            return _tarefas.Max(t => t.Id) + 1;
        }

        public List<Tarefa> ListarPorUsuario(int id)
        {
            return ListarTodas().Where(t => t.Responsavel.Id == id || t.Criador.Id == id).ToList();
        }

        public bool MarcarMembro(Tarefa tarefa, Usuario membro)
        {
            var tarefaEncontrada = BuscarPorID(tarefa.Id);
            if (tarefaEncontrada == null)
                return false;
            
            tarefaEncontrada.Membros.Add(membro);
            return true;
        }
    }
}
