using TarefasLibrary.Modelo;

namespace ApiRest.DTOs
{
    public class TarefaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public StatusTarefa Status { get; set; }
        public UsuarioDTO Criador { get; set; }
        public UsuarioDTO Responsavel { get; set; }
        public List<UsuarioDTO> Membros { get; set; } = new List<UsuarioDTO>();
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime Prazo { get; set; }
        public TimeSpan TempoTotal { get; set; }
        public Tarefa.Prioridade PrioridadeTarefa { get; set; }
    }

    public class CriarTarefaDTO
    {
        public string Titulo { get; set; }
        public StatusTarefa Status { get; set; }
        public int CriadorId { get; set; }
        public int ResponsavelId { get; set; }
        public DateTime Prazo { get; set; }
        public string Descricao { get; set; }
        public Tarefa.Prioridade PrioridadeTarefa { get; set; }
    }

    public class AtualizarTarefaDTO
    {
        public string Titulo { get; set; }
        public StatusTarefa Status { get; set; }
        public int ResponsavelId { get; set; }
        public DateTime Prazo { get; set; }
        public string Descricao { get; set; }
        public Tarefa.Prioridade PrioridadeTarefa { get; set; }
    }
}
