
namespace TarefasLibrary.Modelo
{
    public class Relogio
    {
        public TimeSpan TempoTotal { get; set; }
        public List<Cronometro> Tempos = new();

        public TimeSpan Finaliza()
        {
            if (Tempos.Last().EmAndamento())
                Pause();

            TempoTotal = Tempos.Select(t => t.Total)
                 .Aggregate(TimeSpan.Zero, (acc, curr) => acc + curr);

            return TempoTotal;
        }

        public bool Start()
        {
            if (Tempos.Count > 0 && Tempos.Last().EmAndamento())
                return false;

            Tempos.Add(new Cronometro());
            return true;
        }

        public bool Pause()
        {
            return Tempos.Last().Stop();
        }
    }
}