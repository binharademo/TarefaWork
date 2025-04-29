

using BlazorTarefas.ModelosTeste;

namespace BlazorTarefas.Servicos
{
    public class TarefaService
    {
        private readonly List<Tarefa> _tarefas = new();
        private int _nextId = 1;

        public TarefaService()
        {
            // Adicionar algumas tarefas iniciais para teste
            AddTarefasIniciais();
        }

        private void AddTarefasIniciais()
        {
            // Tarefa 1
            _tarefas.Add(new Tarefa
            {
                Id = _nextId++,
                Titulo = "Estudar Blazor",
                Descricao = "Aprender os conceitos básicos de Blazor e criar um projeto de exemplo",
                Prioridade = "Alta",
                DataCriacao = DateTime.Now.AddDays(-5),
                Concluida = true,
                Status = "DONE"
            });

            // Tarefa 2
            _tarefas.Add(new Tarefa
            {
                Id = _nextId++,
                Titulo = "Implementar CRUD",
                Descricao = "Criar operações de Create, Read, Update e Delete para o gerenciador de tarefas",
                Prioridade = "Alta",
                DataCriacao = DateTime.Now.AddDays(-3),
                Concluida = true,
                Status = "DONE"
            });

            // Tarefa 3
            _tarefas.Add(new Tarefa
            {
                Id = _nextId++,
                Titulo = "Estilizar a interface",
                Descricao = "Melhorar o visual da aplicação usando Bootstrap",
                Prioridade = "Média",
                DataCriacao = DateTime.Now.AddDays(-2),
                Concluida = false,
                Status = "DOING"
            });

            // Tarefa 4
            _tarefas.Add(new Tarefa
            {
                Id = _nextId++,
                Titulo = "Implementar validação de formulários",
                Descricao = "Adicionar validação aos formulários de criação e edição de tarefas",
                Prioridade = "Média",
                DataCriacao = DateTime.Now.AddDays(-1),
                Concluida = false,
                Status = "DOING"
            });

            // Tarefa 5
            _tarefas.Add(new Tarefa
            {
                Id = _nextId++,
                Titulo = "Fazer deploy da aplicação",
                Descricao = "Publicar a aplicação em um servidor web",
                Prioridade = "Baixa",
                DataCriacao = DateTime.Now,
                Concluida = false,
                Status = "TODO"
            });
        }

        public Task<List<Tarefa>> GetTarefasAsync() => Task.FromResult(_tarefas);

        public Task<List<Tarefa>> GetTarefasByStatusAsync(string status) =>
            Task.FromResult(_tarefas.Where(t => t.Status == status).ToList());

        public Task<Tarefa?> GetTarefaByIdAsync(int id) =>
            Task.FromResult(_tarefas.FirstOrDefault(t => t.Id == id));

        public Task AddTarefaAsync(Tarefa tarefa)
        {
            tarefa.Id = _nextId++;
            if (string.IsNullOrEmpty(tarefa.Status))
            {
                tarefa.Status = "TODO";
            }
            _tarefas.Add(tarefa);
            return Task.CompletedTask;
        }

        public Task UpdateTarefaAsync(Tarefa tarefa)
        {
            var existente = _tarefas.FirstOrDefault(t => t.Id == tarefa.Id);
            if (existente != null)
            {
                existente.Titulo = tarefa.Titulo;
                existente.Descricao = tarefa.Descricao;
                existente.Concluida = tarefa.Concluida;
                existente.Prioridade = tarefa.Prioridade;
                existente.Status = tarefa.Status;
            }
            return Task.CompletedTask;
        }

        public Task DeleteTarefaAsync(int id)
        {
            var tarefa = _tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa != null) _tarefas.Remove(tarefa);
            return Task.CompletedTask;
        }
    }
}
