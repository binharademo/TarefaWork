using TarefasLibrary.Modelo;

namespace TarefasLibrary.Interface
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        List<Usuario> Listar(Usuario.Setor  setor);
        List<Usuario> Listar(Usuario.Funcao funcao);
    }
}