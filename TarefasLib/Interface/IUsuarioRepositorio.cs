using TarefasLibrary.Modelo;

namespace TarefasLibrary.Interface
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        List<Usuario> Listar(Setor SetorUsuario);
        List<Usuario> Listar(Usuario.Funcao funcao);
    }
}