namespace TarefasLibrary.Modelo
{
    public class Cronometro
    {
        public int Id { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public TimeSpan Total { get; set; }

        public Cronometro()
        {
            Inicio = DateTime.Now;
        }

        public bool Stop()
        {
            if (!EmAndamento())
                return false;

            Fim = DateTime.Now;
            Total = Fim.Value.Subtract(Inicio);

            return true;
        }

        public bool EmAndamento(){
            return !Fim.HasValue;
        }
    }
}