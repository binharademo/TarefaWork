using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Negocio
{
    public class UsuarioServico : IUsuarioServico
    {
        IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio)
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

        public List<Usuario> ListarUsuarioPorSetor(Setor setorusuario)
        {
            return _usuarioRepositorio.Listar(setorusuario);
        }

        public List<Usuario> ListarUsuarioPorFuncao(Usuario.Funcao funcao)
        {
            return _usuarioRepositorio.Listar(funcao);
        }

        public bool Editar(int id, string nome, string senha, Usuario.Funcao funcao, Setor setorusuario)
        {
            return _usuarioRepositorio.Editar(new(id, nome, senha, funcao, setorusuario));
        }

        public bool Editar(Usuario usuario)
        {
            return _usuarioRepositorio.Editar(usuario);
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
