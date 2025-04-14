using TarefasLibrary.Modelo;

namespace TarefasLibrary.Interface
{
    public interface IUsuarioRepositorio<T> : IRepositorio<T>
    {
        List<T> Listar(Usuario.Setor  setor);
        List<T> Listar(Usuario.Funcao funcao);
    }
}