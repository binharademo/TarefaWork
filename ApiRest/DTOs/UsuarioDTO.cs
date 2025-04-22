using TarefasLibrary.Modelo;
using static TarefasLibrary.Modelo.Usuario;
using Setor = TarefasLibrary.Modelo.Usuario.Setor;

namespace ApiRest.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Funcao FuncaoUsuario { get; set; }
        public Setor SetorUsuario { get; set; }

    }

    public class CriarUsuarioDTO
    {
        public string Nome { get; set; }
        public string Senha {  get; set; }
        public Funcao FuncaoUsuario { get; set; }
        public Setor SetorUsuario { get; set; }
    }
    public class AtualizarUsuarioDTO
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public Funcao FuncaoUsuario { get; set; }
        public Setor SetorUsuario { get; set; }
    }
}
