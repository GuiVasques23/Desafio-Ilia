using System;
using System.Collections.Generic;

namespace Time.Sheet.Domain.Models
{
    public class Registro
    {
        public int Id { get; set; }
        public DateTime Dia { get; set; }
        public List<Horario> Horarios { get; set; } = new List<Horario>();
    }
}