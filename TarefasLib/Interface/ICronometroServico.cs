namespace TestsTarefas
{
    public interface ICronometroServico
    {
        void Iniciar();
        void Pausar();
        TimeSpan ObterTempoDecorrido();
        CronometroServico ObterCronometro();
    }
}