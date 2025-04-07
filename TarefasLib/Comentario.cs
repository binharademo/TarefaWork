namespace Tarefas_Library
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public DateTime DataCriacao { get; set; }


        public Comentario(string descricao, DateTime dataCriacao)
        {
            this.Descricao = descricao;
            this.DataCriacao = dataCriacao;
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
            this.Id = ++contadorIDs;
            Comentario.ListaComentarios.Add(this);
        }

        public bool BuscarComentario()
        {
            foreach (var comentario in ListaComentarios)
            {
                if (comentario.Id == this.Id)
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