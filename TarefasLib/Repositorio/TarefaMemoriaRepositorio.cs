using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Repositorio
{
    public class TarefaMemoriaRepositorio : ITarefaRepositorio
    {
        
        private readonly List<Tarefa> _tarefas = new List<Tarefa>();

        public bool Salvar(Tarefa tarefa)
        {
            // TODO: Validar o parâmetro tarefa antes de salvar (null check)
            // TODO: Verificar se a tarefa já existe na lista antes de adicionar
            tarefa.Id = GeraNovoId();
            _tarefas.Add(tarefa);
            return true;
        }

        public List<Tarefa> ListarTodas()
        {
            // TODO: Retornar uma cópia da lista para evitar modificações externas
            // TODO: Considerar retornar IReadOnlyCollection<Tarefa> para evitar modificações externas
            return _tarefas;
        }

        public Tarefa? BuscarPorID(int id)
        {
            // TODO: Usar LINQ para simplificar a busca (FirstOrDefault)
            foreach (var tarefa in _tarefas)
            {
                if (tarefa.Id == id && id > 0)
                    return tarefa;
            }

            return null;
        }

        public bool Atualizar(Tarefa tarefa, Tarefa.Status novostatus, string novadescricao, DateTime novoprazo, string novotitulo, Tarefa.Prioridade novaprioridade)
        {
            // TODO: Validar todos os parâmetros antes de atualizar (null checks e validações de negócio)
            if(novostatus == null)
            {
                return false;
            }
            // TODO: Salvar as alterações na tarefa da lista de tarefas. 
            // TODO: Verificar se a tarefa existe no repositório antes de atualizar
            // TODO: Considerar criar uma cópia da tarefa para evitar modificações externas durante a atualização
            tarefa.PrioridadeTarefa = novaprioridade;
            tarefa.Titulo = novotitulo;
            tarefa.StatusTarefa = novostatus;
            tarefa.Descricao = novadescricao;
            tarefa.Prazo = novoprazo;
            return true;
        }

        public bool Atualizar(Tarefa tarefa, Tarefa.Status novostatus)
        {
            // TODO: Validar os parâmetros tarefa e novostatus (null checks)
            if (novostatus == null)
            {
                return false;
            }
            // TODO: Salvar as alterações na tarefa da lista de tarefas. 
            // TODO: Verificar se a tarefa existe no repositório antes de atualizar
            tarefa.StatusTarefa = novostatus;
            return true;
        }

        public bool Atualizar(Tarefa tarefa)
        {
            // TODO: Salvar as alterações na tarefa da lista de tarefas. 
            return true;
        }

        private int GeraNovoId()
        {
            // TODO: Considerar usar um mecanismo mais robusto para geração de IDs (ex: GUID)
            if (_tarefas.Count == 0) return 1;
            return _tarefas.Max(t => t.Id) + 1;
        }

        public List<Tarefa> ListarPorUsuario(int id)
        {
            // TODO: Validar o id do usuário (deve ser maior que zero)
            // TODO: Considerar retornar IReadOnlyCollection<Tarefa> para evitar modificações externas
            return ListarTodas().Where(t => t.Responsavel.Id == id || t.Criador.Id == id).ToList();
        }

        public bool MarcarMembro(Tarefa tarefa, Usuario membro)
        {
            // TODO: Validar os parâmetros tarefa e membro (null checks)
            var tarefaEncontrada = BuscarPorID(tarefa.Id);
            if (tarefaEncontrada == null)
                return false;

            // TODO: Salvar as alterações na tarefa da lista de tarefas. 
            // TODO: Verificar se o membro já está na lista antes de adicionar
            tarefaEncontrada.Membros.Add(membro);
            return true;
        }

        public List<Tarefa> Buscar(FiltroTarefa filtro)
        {
            // TODO: Validar o parâmetro filtro (null check)
            // TODO: Considerar extrair a lógica de filtro para métodos separados para melhorar a legibilidade
            // TODO: Considerar retornar IReadOnlyCollection<Tarefa> para evitar modificações externas
            return _tarefas.Where(t => 
                   (string.IsNullOrEmpty(filtro.Nome) ? true : t.Titulo.Contains(filtro.Nome))
                && (filtro.Prioridade is null || t.PrioridadeTarefa == filtro.Prioridade)
                && (filtro.Status is null || t.StatusTarefa == filtro.Status)
                && (filtro.Criador is null || t.Criador.Id == filtro.Criador)
                && (filtro.Responsavel is null || t.Responsavel.Id == filtro.Responsavel)
                && (filtro.Membro is null || t.Membros.Exists(m => m.Id == filtro.Membro ))
                && (filtro.Inicio is null || t.DataCriacao >= filtro.Inicio)
                && (filtro.Fim is null || t.DataCriacao <= filtro.Fim)
            ).ToList();
        }
    }
}
