using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Negocio
{
    // TODO: Considerar separar as responsabilidades de gerenciamento de tarefas e cronometragem (SRP - Single Responsibility Principle)
    public class TarefaServico : ITarefaServico, ICronometroServico<Tarefa>
    {
        // TODO: Tornar o campo readonly para garantir imutabilidade
        // TODO: Usar convenção de nomenclatura para campos privados (_repositorio)
        ITarefaRepositorio _repositorio;

        public TarefaServico(ITarefaRepositorio repositorio)
        {
            // TODO: Validar o parâmetro repositorio (null check)
            _repositorio = repositorio;
        }

        public bool Salvar(Tarefa tarefa)
        {
            // TODO: Validar o parâmetro tarefa antes de salvar (null check e validações de negócio)
            return _repositorio.Salvar(tarefa);
        }

        public Tarefa? BuscarPorId(int id)
        {
            // TODO: Validar o id (deve ser maior que zero)
            return _repositorio.BuscarPorID(id);
        }

        public bool Atualizar(Tarefa tarefa, Tarefa.Status novostatus, string novadescricao, DateTime novoprazo, string novotitulo, Tarefa.Prioridade novaprioridade)
        {
            // TODO: Validar todos os parâmetros antes de atualizar (null checks e validações de negócio)
            // TODO: Considerar usar um objeto DTO para encapsular os parâmetros de atualização
            return _repositorio.Atualizar(tarefa, novostatus, novadescricao, novoprazo, novotitulo, novaprioridade);    
        }

        public bool Atualizar(Tarefa tarefa, Tarefa.Status novostatus)
        {
            // TODO: Validar os parâmetros tarefa e novostatus (null checks)
            return _repositorio.Atualizar(tarefa, novostatus);
        }

        // TODO: Retornar IReadOnlyCollection<Tarefa> para evitar modificações externas da coleção
        public List<Tarefa> ListarTodas()
        {
            // TODO: Considerar implementar paginação para grandes volumes de dados
            return _repositorio.ListarTodas();
        }

        public List<Tarefa> ListarPorUsuario(int id)
        {
            // TODO: Validar o id do usuário (deve ser maior que zero)
            return _repositorio.ListarPorUsuario(id);
        }

        public bool MarcarMembro(Tarefa tarefa, Usuario membro)
        {
            // TODO: Validar os parâmetros tarefa e membro (null checks)
            var tarefaPesquisa = BuscarPorId(tarefa.Id);
            if (tarefaPesquisa == null)
                return false;

            if (tarefaPesquisa.Membros.Contains(membro))
                return false;

            // TODO: Considerar adicionar validações de negócio adicionais (ex: verificar se o usuário tem permissão)
            return _repositorio.MarcarMembro(tarefa, membro);
        }

        // TODO: Esta funcionalidade deveria estar em uma classe separada (SRP - Single Responsibility Principle)
        public TimeSpan PausaCronometro(Tarefa tarefa)
        {
            // TODO: Validar o parâmetro tarefa (null check)
            if (tarefa.Tempos.Count == 0) 
                return TimeSpan.Zero;

            if (!tarefa.Tempos.Last().EmAndamento())
                return tarefa.TempoTotal;

            tarefa.Tempos.Last().Stop();
            // TODO: Extrair este cálculo para um método separado para melhorar a legibilidade
            tarefa.TempoTotal = tarefa.Tempos.Select(t => t.Total)
                 .Aggregate(TimeSpan.Zero, (acc, curr) => acc + curr);

            return tarefa.TempoTotal;
        }

        // TODO: Esta funcionalidade deveria estar em uma classe separada (SRP - Single Responsibility Principle)
        public bool IniciaCronometro(Tarefa tarefa)
        {
            // TODO: Validar o parâmetro tarefa (null check)
            if (tarefa.Tempos.Count > 0 && tarefa.Tempos.Last().EmAndamento())
                return false;

            tarefa.Tempos.Add(new Cronometro());
            return true;
        }

        public bool Finalizar(Tarefa tarefa01)
        {
            // TODO: Validar o parâmetro tarefa01 (null check)
            // TODO: Renomear o parâmetro para 'tarefa' para manter consistência com os outros métodos
            PausaCronometro(tarefa01);
            tarefa01.StatusTarefa = Tarefa.Status.Done;

            // TODO: Verificar o resultado da atualização e retornar false em caso de falha
            _repositorio.Atualizar(tarefa01, Tarefa.Status.Done);
            return true;
        }

        // TODO: Retornar IReadOnlyCollection<Tarefa> para evitar modificações externas da coleção
        public List<Tarefa> Busca(FiltroTarefa filtro)
        {
            // TODO: Validar o parâmetro filtro (null check)
            // TODO: Considerar implementar paginação para grandes volumes de dados
            return _repositorio.Buscar(filtro);
        }
    }
}
