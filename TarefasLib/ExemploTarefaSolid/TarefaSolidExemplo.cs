namespace TarefasLibrary.ExemploTarefaSolid
{
    public class TarefaSolidExemplo
    {
        public TarefaSolidExemplo(int id, string titulo, string status, string criador, string responsavel,
                     DateTime prazo, string descricao, DateTime? dataCriacao = null)
        {
            ValidarDados(titulo, status);

            Id = id;
            Titulo = titulo;
            Status = status;
            Criador = criador;
            Responsavel = responsavel;
            Prazo = prazo;
            DataCriacao = dataCriacao ?? DateTime.Now;
            Descricao = descricao;
        }

        public TarefaSolidExemplo(int id)
        {
            Id = id;
        }

        private void ValidarDados(string titulo, string status)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("O título da tarefa não pode ser vazio", nameof(titulo));

            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("O status da tarefa não pode ser vazio", nameof(status));
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Status { get; set; }
        public string Criador { get; set; }
        public string Responsavel { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime Prazo { get; set; }
    }
}
