namespace Time.Sheet.Domain.Models
{
    public class Horario
    {
        public TimeSpan DataHora { get; set; }

        public Horario() { }

        public Horario(TimeSpan dataHora)
        {
            DataHora = dataHora;
        }
    }
}

