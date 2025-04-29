namespace BlazorTarefas.ModelosTeste
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public bool Concluida { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public string Prioridade { get; set; } = "Baixa";
        public string Status { get; set; } = "TODO";
    }
}
