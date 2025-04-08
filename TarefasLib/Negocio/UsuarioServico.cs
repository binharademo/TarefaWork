using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Negocio
{
    public class UsuarioServico : IUsuarioServico
    {
        IRepositorio<Usuario> _usuarioRepositorio;

        public UsuarioServico(IRepositorio<Usuario> usuarioRepositorio) {
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
            return _usuarioRepositorio.Listar();
        }

        public bool Editar(int id, string nome, string senha, string funcao, string setor)
        {
            return _usuarioRepositorio.Editar(new(id, nome, senha, funcao, setor));
        }

    }
}
