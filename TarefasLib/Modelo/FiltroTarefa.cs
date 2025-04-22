namespace TarefasLibrary.Modelo
{
    public class FiltroTarefa
    {
        public string? Nome { get; set; }
        public Tarefa.Prioridade? Prioridade { get; set; }
        public Tarefa.Status? Status { get; set; }

        public int? Criador { get; set; } // id do usuario
        public int? Responsavel{ get; set; }
        public int? Membro { get; set; }

        // Range para data de criação
        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }

    }
}
