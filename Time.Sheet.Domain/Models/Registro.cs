using System.ComponentModel.DataAnnotations;

namespace Time.Sheet.Domain.Models
{
    public class Registro
    {
        [Key]
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dia { get; set; }
        public Horario[] Horarios { get; set; } = new Horario[4];
    }
}