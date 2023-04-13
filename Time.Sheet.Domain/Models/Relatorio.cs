namespace Time.Sheet.Domain.Models
{
    public class Relatorio
    {
        public string Mes { get; set; }
        public List<Registro> Registros { get; set; }
    }
}
