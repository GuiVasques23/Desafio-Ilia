namespace Time.Sheet.Domain.Models
{
    public class Horario
    {
        public DateTime DataHora { get; set; }

        public Horario() { }

        public Horario(DateTime dataHora)
        {
            DataHora = dataHora;
        }
    }
}

