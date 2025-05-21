using TarefasLibrary.Modelo;
using static TarefasLibrary.Modelo.Usuario;

namespace ApiRest.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Funcao FuncaoUsuario { get; set; }
        public int SetorUsuarioId { get; set; }
        public string? SetorNome { get; set; } // Opcional, só para exibição
    }

    public class CriarUsuarioDTO
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public Funcao FuncaoUsuario { get; set; }
        public int SetorUsuarioId { get; set; }
    }

    public class AtualizarUsuarioDTO
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public Funcao FuncaoUsuario { get; set; }
        public int SetorUsuarioId { get; set; }
    }


}
