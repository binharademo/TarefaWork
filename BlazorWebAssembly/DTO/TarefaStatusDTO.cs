namespace BlazorWebAssembly.DTO
{
    public class TarefaStatusDTO(int id, string nome, TarefaStatusTipo tipo)
    {
        public int Id { get; set; } = id;
        public string Nome { get; set; } = nome;
        public TarefaStatusTipo Tipo { get; set; } = tipo;
    }

    public enum TarefaStatusTipo
    {
        WAITING, IN_PROGRESS, FINISHED
    }
}
