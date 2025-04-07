
namespace Tarefas_Library
{
    public interface IUsuarioRepositorio
    {
        Usuario? BuscarPorId(int id);
        bool Cadastrar(Usuario usuario);
        List<Usuario> ListarUsuario();
    }
}