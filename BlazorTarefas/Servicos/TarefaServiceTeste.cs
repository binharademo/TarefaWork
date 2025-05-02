

using BlazorTarefas.ModelosTeste;
using ApiRest.DTOs;
using TarefasLibrary.Modelo;

namespace BlazorTarefas.Servicos
{
    public class TarefaService
    {
        //private readonly List<Tarefa> _tarefas = new();
        private readonly  List<TarefaTeste> _tarefas = new ();
        private readonly UsuarioServico _usuarioServico;
        private int _nextId = 1;

        public TarefaService(UsuarioServico servico)
        {
            // Adicionar algumas tarefas iniciais para teste
            _usuarioServico = servico;
            AddTarefasIniciais();
            
        }


        private void AddTarefasIniciais()
        {
            
            _usuarioServico.Adicionar(new CriarUsuarioDTO
            {
                Nome = "Criador",
                FuncaoUsuario = Usuario.Funcao.Dev,
                SetorUsuario = Usuario.Setor.Ti
            });
            _usuarioServico.Adicionar(new CriarUsuarioDTO
            {
                Nome = "Responsavel",
                FuncaoUsuario = Usuario.Funcao.Analista,
                SetorUsuario = Usuario.Setor.Marketing
            });
            var usuarios = _usuarioServico.BuscaTodos().Result;
            // Tarefa 1
            _tarefas.Add(new TarefaTeste
            {
                Id = _nextId++,
                Titulo = "Estudar Blazor",
                Descricao = "Aprender os conceitos básicos de Blazor e criar um projeto de exemplo",
                DataCriacao = DateTime.Now.AddDays(-5),
                Criador = usuarios[0],
                Responsavel = usuarios[1]

            });

            // Tarefa 2
            _tarefas.Add(new TarefaTeste
            {
                Id = _nextId++,
                Titulo = "Implementar CRUD",
                Descricao = "Criar operações de Create, Read, Update e Delete para o gerenciador de tarefas",
                Prioridade = "Alta",
                DataCriacao = DateTime.Now.AddDays(-3),
                Concluida = true,
                Status = "DONE",
                Criador = usuarios[1],
                Responsavel = usuarios[0]
            });

            // Tarefa 3
            _tarefas.Add(new TarefaTeste
            {
                Id = _nextId++,
                Titulo = "Estilizar a interface",
                Descricao = "Melhorar o visual da aplicação usando Bootstrap",
                Prioridade = "Média",
                DataCriacao = DateTime.Now.AddDays(-2),
                Concluida = false,
                Status = "DOING",
                Criador = usuarios[0],
                Responsavel = usuarios[1]
            });

            // Tarefa 4
            _tarefas.Add(new TarefaTeste
            {
                Id = _nextId++,
                Titulo = "Implementar validação de formulários",
                Descricao = "Adicionar validação aos formulários de criação e edição de tarefas",
                Prioridade = "Média",
                DataCriacao = DateTime.Now.AddDays(-1),
                Concluida = false,
                Status = "DOING",
                Criador = usuarios[1],
                Responsavel = usuarios[0]
            });

            // Tarefa 5
            _tarefas.Add(new TarefaTeste
            {
                Id = _nextId++,
                Titulo = "Fazer deploy da aplicação",
                Descricao = "Publicar a aplicação em um servidor web",
                Prioridade = "Baixa",
                DataCriacao = DateTime.Now,
                Concluida = false,
                Status = "TODO",
                Criador = usuarios[1],
                Responsavel = usuarios[1]
            });
        }

        public Task<List<TarefaTeste>> GetTarefasAsync() => Task.FromResult(_tarefas);

        public Task<List<TarefaTeste>> GetTarefasByStatusAsync(string status) =>
            Task.FromResult(_tarefas.Where(t => t.Status == status).ToList());

        public Task<TarefaTeste?> GetTarefaByIdAsync(int id) =>
            Task.FromResult(_tarefas.FirstOrDefault(t => t.Id == id));

        public Task AddTarefaAsync(TarefaTeste tarefa)
        {
            tarefa.Id = _nextId++;
            if (string.IsNullOrEmpty(tarefa.Status))
            {
                tarefa.Status = "TODO";
            }
            _tarefas.Add(tarefa);
            return Task.CompletedTask;
        }

        public Task UpdateTarefaAsync(TarefaTeste tarefa)
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
