using TarefasLibrary.Modelo;

namespace ApiRest.DTOs
{
    public class TarefaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public Tarefa.Status Status { get; set; }
        public int CriadorId { get; set; }
        public int ResponsavelId { get; set; }
        //public List<UsuarioDTO> Membros { get; set; } = new List<UsuarioDTO>();
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
            CriadorId = tarefa.CriadorId;
            ResponsavelId = tarefa.ResponsavelId;
            //Membros = tarefa.Membros.Select(m => new UsuarioDTO
            //{
            //    Id = m.Id,
            //    Nome = m.Nome,
            //    FuncaoUsuario = m.FuncaoUsuario,
            //    SetorUsuario = m.SetorUsuario
            //}).ToList();
            Descricao = tarefa.Descricao;
            DataCriacao = tarefa.DataCriacao;
            Prazo = tarefa.Prazo;
            TempoTotal = tarefa.TempoTotal;
            PrioridadeTarefa = tarefa.PrioridadeTarefa;
        }

        public TarefaDTO() { }
    }

    public class TarefaBasicoDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public Tarefa.Status Status { get; set; }
        public int CriadorId { get; set; }
        public int ResponsavelId { get; set; }
        //public List<int> Membros { get; set; } = new List<int>();
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
            CriadorId = tarefa.CriadorId;
            ResponsavelId = tarefa.ResponsavelId;
            //Membros = tarefa.Membros;
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
