using ApiRest.DTOs;
using TarefasLibrary.Modelo;

namespace BlazorTarefas.Servicos
{
    public class TarefaServico
    {
        private List<TarefaDTO> _tarefa = new List<TarefaDTO>();
        private readonly UsuarioServico _usuario;
        private int _nextId = 1;

        public TarefaServico(UsuarioServico usuario)
        {
            _usuario = usuario;
        }

        public async Task <Boolean> Adicionar(CriarTarefaDTO tarefa)
        {
            var criador = await _usuario.BuscaPorId(tarefa.CriadorId);
            var responsavel = await _usuario.BuscaPorId(tarefa.ResponsavelId);
            _tarefa.Add(new TarefaDTO
            {
                Id = _nextId++,
                Titulo = tarefa.Titulo,
                Status = tarefa.Status,
                Criador = criador,
                Responsavel = responsavel,
                Prazo = tarefa.Prazo,
                Descricao = tarefa.Descricao,
                PrioridadeTarefa = tarefa.PrioridadeTarefa

            });
            return true;
        }

        public Boolean Atualizar(TarefaDTO tarefa)
        {
            var tarefaExistente = _tarefa.FirstOrDefault(t => t.Id == tarefa.Id);
            if (tarefaExistente != null)
            {
                tarefaExistente.Titulo = tarefa.Titulo;
                tarefaExistente.Status = tarefa.Status;
                tarefaExistente.Prazo = tarefa.Prazo;
                tarefaExistente.Descricao = tarefa.Descricao;
                tarefaExistente.PrioridadeTarefa = tarefa.PrioridadeTarefa;
                return (true);
            }
            return (false);
        }

        public Task<List<TarefaDTO>> BuscaTodos()
        {
            return Task.FromResult(_tarefa);
        }

        public Task<TarefaDTO?> BuscaPorId(int id) =>
            Task.FromResult(_tarefa.FirstOrDefault(t => t.Id == id));

        public Task Remover(int id)
        {
            var tarefa = _tarefa.FirstOrDefault(t => t.Id == id);
            if (tarefa != null) _tarefa.Remove(tarefa);
            return Task.CompletedTask;
        }

        public Task<List<TarefaDTO>> BuscaPorStatus(Tarefa.Status status ) =>
         Task.FromResult(_tarefa.Where(t => t.Status == status).ToList());


    }
}
