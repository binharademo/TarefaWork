using TarefasLibrary.Modelo;

namespace ApiRest.DTOs
{
    public class TarefaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public Tarefa.Status Status { get; set; }
        public UsuarioDTO Criador { get; set; }
        public UsuarioDTO Responsavel { get; set; }
        public List<UsuarioDTO> Membros { get; set; } = new List<UsuarioDTO>();
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime Prazo { get; set; }
        public TimeSpan TempoTotal { get; set; }
        public Tarefa.Prioridade PrioridadeTarefa { get; set; }

        public TarefaDTO(Tarefa tarefa)
        {
            Id = tarefa.Id;
            Titulo = tarefa.Titulo;
            Status = tarefa.StatusTarefa;
            Criador = new UsuarioDTO
            {
                Id = tarefa.Criador.Id,
                Nome = tarefa.Criador.Nome,
                FuncaoUsuario = tarefa.Criador.FuncaoUsuario,
                SetorUsuario = tarefa.Criador.SetorUsuario
            };
            Responsavel = new UsuarioDTO
            {
                Id = tarefa.Responsavel.Id,
                Nome = tarefa.Responsavel.Nome,
                FuncaoUsuario = tarefa.Responsavel.FuncaoUsuario,
                SetorUsuario = tarefa.Responsavel.SetorUsuario
            };
            Membros = tarefa.Membros.Select(m => new UsuarioDTO
            {
                Id = m.Id,
                Nome = m.Nome,
                FuncaoUsuario = m.FuncaoUsuario,
                SetorUsuario = m.SetorUsuario
            }).ToList();
            Descricao = tarefa.Descricao;
            DataCriacao = tarefa.DataCriacao;
            Prazo = tarefa.Prazo;
            TempoTotal = tarefa.TempoTotal;
            PrioridadeTarefa = tarefa.PrioridadeTarefa;
        }
    }

    public class TarefaBasicoDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public Tarefa.Status Status { get; set; }
        public UsuarioBasicoDTO Criador { get; set; }
        public UsuarioBasicoDTO Responsavel { get; set; }
        public List<UsuarioBasicoDTO> Membros { get; set; } = new List<UsuarioBasicoDTO>();
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime Prazo { get; set; }
        public TimeSpan TempoTotal { get; set; }
        public Tarefa.Prioridade PrioridadeTarefa { get; set; }

        public TarefaBasicoDTO() { }

        public TarefaBasicoDTO(Tarefa tarefa)
        {
            Id = tarefa.Id;
            Titulo = tarefa.Titulo;
            Status = tarefa.StatusTarefa;
            Criador = new UsuarioBasicoDTO
            {
                Id = tarefa.Criador.Id,
                Nome = tarefa.Criador.Nome,
            };
            Responsavel = new UsuarioBasicoDTO
            {
                Id = tarefa.Responsavel.Id,
                Nome = tarefa.Responsavel.Nome,
            };
            Membros = tarefa.Membros.Select(m => new UsuarioBasicoDTO
            {
                Id = m.Id,
                Nome = m.Nome,
            }).ToList();
            Descricao = tarefa.Descricao;
            DataCriacao = tarefa.DataCriacao;
            Prazo = tarefa.Prazo;
            TempoTotal = tarefa.TempoTotal;
            PrioridadeTarefa = tarefa.PrioridadeTarefa;
        }
    }

    public class CriarTarefaDTO
    {
        public string Titulo { get; set; }
        public Tarefa.Status Status { get; set; }
        public int CriadorId { get; set; }
        public int ResponsavelId { get; set; }
        public DateTime Prazo { get; set; }
        public string Descricao { get; set; }
        public Tarefa.Prioridade PrioridadeTarefa { get; set; }
    }

    public class AtualizarTarefaDTO
    {
        public string Titulo { get; set; }
        public Tarefa.Status Status { get; set; }
        public int ResponsavelId { get; set; }
        public DateTime Prazo { get; set; }
        public string Descricao { get; set; }
        public Tarefa.Prioridade PrioridadeTarefa { get; set; }
    }
}
