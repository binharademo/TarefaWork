using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Negocio
{
    public class UsuarioServico : IUsuarioServico
    {
        IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio) {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public Usuario? Buscar(int id)
        {
            return _usuarioRepositorio.BuscarPorId(id);
        }

        public Usuario Criar(Usuario usuario)
        {
            if (_usuarioRepositorio.Cadastrar (usuario))
                return usuario;

            return null;
        }

        public List<Usuario> ListarUsuario()
        {
            return _usuarioRepositorio.ListarUsuario();
        }

        public bool? Editar(int id, string nome, string senha, string funcao, string setor)
        {
            if (_usuarioRepositorio.Editar( id, nome, senha, funcao, setor))
                return true;
            return null;
        }
    }
}
