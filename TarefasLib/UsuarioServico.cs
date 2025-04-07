

namespace Tarefas_Library
{
    public class UsuarioServico
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
    }
}
