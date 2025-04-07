using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Repositorio
{
    public class UsuarioMemoriaRepositorio : IUsuarioRepositorio
    {
        private List<Usuario> ListaUsuarios = new List<Usuario>();

        public bool Cadastrar(Usuario usuario)
        {
            usuario.Id = GeraNovoId();
            ListaUsuarios.Add(usuario);
            return true;
        }

        public List<Usuario> ListarUsuario()
        {
            return ListaUsuarios;
        }

        public Usuario? BuscarPorId(int id)
        {
            foreach (var usuario in ListaUsuarios)
            {
                if (usuario.Id == id)
                    return usuario;
            }
            return null;
        }

        public bool Editar(int id,string nome,string senha, string funcao, string setor)
        {
            var usuarioExistente = BuscarPorId(id);
            if (usuarioExistente != null)
            {
                usuarioExistente.Nome = nome;
                usuarioExistente.Senha = senha;
                usuarioExistente.Funcao = funcao;
                usuarioExistente.Setor =setor;
                return true;
            }
            return false;
        }

        private int GeraNovoId()
        {
            if (ListaUsuarios.Count == 0) return 1;

            return ListaUsuarios.Max(u => u.Id) + 1;
        }
    }
}
