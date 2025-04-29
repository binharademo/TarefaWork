using TarefasLibrary.Modelo;

namespace ApiRest.DTOs
{
    public class ComentarioDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public int TarefaId { get; set; }
        public int UsuarioId { get; set; }

        public ComentarioDTO(Comentario comentario)
        {
            Id = comentario.Id;
            Descricao = comentario.Descricao;
            DataCriacao = comentario.DataCriacao;
            TarefaId = comentario.TarefaId;
            UsuarioId = comentario.UsuarioId;
        }
    }
    public class ComentarioCriarDTO
    {
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public int TarefaId { get; set; }
        public int UsuarioId { get; set; }
    }

    public class ComentarioAtualizarDTO
    {
        public string Descricao { get; set; }
    }
}
