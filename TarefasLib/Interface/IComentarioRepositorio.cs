using TarefasLibrary.Modelo;

namespace TarefasLibrary.Interface
{
    public interface IComentarioRepositorio : IRepositorio<Comentario>
    {
        List<Comentario> BuscarPorTarefa(int id);
    }
}