using TarefasLibrary.Modelo;

namespace TarefasLibrary.Interface
{
    public interface ICronometroServico<T>
    {
        bool IniciaCronometro(T obj);
        TimeSpan PausaCronometro(T obj);
    }
}