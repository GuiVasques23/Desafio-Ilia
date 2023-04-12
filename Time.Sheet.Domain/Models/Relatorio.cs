using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time.Sheet.Domain.Models
{
    public partial class Relatorio
    {
        public string Mes { get; set; }
        public string HorasTrabalhadas { get; set; }
        public string HorasExcedentes { get; set; }
        public string HorasDevidas { get; set; }
        public List<Registro> Registros { get; set; }
    }
}
