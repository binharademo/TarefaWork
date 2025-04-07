
using TarefasLibrary;

namespace TarefasLibrary.Interface
{
    public interface IUsuarioRepositorio
    {
        Usuario? BuscarPorId(int id);
        bool Cadastrar(Usuario usuario);
        List<Usuario> ListarUsuario();
    }
}