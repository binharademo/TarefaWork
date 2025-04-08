namespace TarefasLibrary.Interface
{
    public interface IRepositorio<T>
    {
        T? BuscarPorId(int id);
        bool Cadastrar(T obj);
        List<T> Listar();
        bool Editar(T obj);

        bool Remover(T obj);

    }
}