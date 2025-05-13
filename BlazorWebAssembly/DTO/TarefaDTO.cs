namespace BlazorWebAssembly.DTO
{
    public class TarefaDTO
    {
        public int Id { get; set; } = 0;
        public string Titulo { get; set; }
        public int Status { get; set; }
        public int CriadorId { get; set; }
        public int ResponsavelId { get; set; }
        //public List<int> Membros { get; set; } = new List<int>();
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.MinValue;
        public DateTime Prazo { get; set; }
        public TimeSpan TempoTotal { get; set; } = TimeSpan.MinValue;
        public int PrioridadeTarefa { get; set; }
    }
}
