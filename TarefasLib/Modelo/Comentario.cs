namespace TarefasLibrary.Modelo
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public DateTime DataCriacao { get; set; }

        public int TarefaId { get; set; }
        public int UsuarioId { get; set; }
        public Comentario(string descricao, DateTime dataCriacao, int tarefaId, int usuarioId )
        {
            Descricao = descricao;
            DataCriacao = dataCriacao;
            TarefaId = tarefaId;
            UsuarioId = usuarioId;
        }

        public Comentario(string descricao, DateTime dataCriacao)
        {
            Descricao = descricao;
            DataCriacao = dataCriacao;

        }

        public Comentario(int id)
        {
            Id = id;
        }

        public Comentario() { }

        public static List<Comentario> ListaComentarios = new List<Comentario>();
        private static int contadorIDs = 0;

        public void SalvarComentario()
        {
            Id = ++contadorIDs;
            ListaComentarios.Add(this);
        }

        public bool BuscarComentario()
        {
            foreach (var comentario in ListaComentarios)
            {
                if (comentario.Id == Id)
                {
                    Id = comentario.Id;
                    Descricao = comentario.Descricao;
                    DataCriacao = comentario.DataCriacao;
                    return true;
                }

            }
            return false;
        }

    }
}