namespace BlazorWebAssembly.DTO
{
    public class TarefaDTO
    {
        public int Id { get; set; } = 0;
        public string Titulo { get; set; } = "";
        public int Status { get; set; } = -1;
        public int CriadorId { get; set; } = -1;
        public int ResponsavelId { get; set; } = -1;
        //public List<int> Membros { get; set; } = new List<int>();
        public string Descricao { get; set; } = "";
        public DateTime DataCriacao { get; set; } = DateTime.MinValue;
        public DateTime Prazo { get; set; } = DateTime.MinValue;
        public TimeSpan TempoTotal { get; set; } = TimeSpan.MinValue;
        public int PrioridadeTarefa { get; set; } = 0;
    }
}
