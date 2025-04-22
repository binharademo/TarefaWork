using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Negocio
{
    public class UsuarioServico : IUsuarioServico
    {
        IUsuarioRepositorio<Usuario> _usuarioRepositorio;

        public UsuarioServico(IUsuarioRepositorio<Usuario> usuarioRepositorio)
        {
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

        public List<Usuario> ListarUsuarioPorSetor(Usuario.Setor setor)
        {
            return _usuarioRepositorio.Listar(setor);
        }

        public List<Usuario> ListarUsuarioPorFuncao(Usuario.Funcao funcao)
        {
            return _usuarioRepositorio.Listar(funcao);
        }

        public bool Editar(int id, string nome, string senha, Usuario.Funcao funcao, Usuario.Setor setor)
        {
            return _usuarioRepositorio.Editar(new(id, nome, senha, funcao, setor));
        }

        public bool Remover(Usuario usuario)
        {
            var existente = Buscar(usuario.Id);

            if (existente == null)
                return false; 

            _usuarioRepositorio.Remover(usuario);
            return true;
        }
    }
}
