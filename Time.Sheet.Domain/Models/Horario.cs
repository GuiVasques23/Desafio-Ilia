using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time.Sheet.Domain.Models
{
    public class Horario
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public TipoHorario Tipo { get; set; }
        public Registro Registro { get; set; }

        public Horario() { }

        public Horario(DateTime dataHora)
        {
            DataHora = dataHora;
            Tipo = ObterTipoHorario(dataHora.TimeOfDay);
        }

        private TipoHorario ObterTipoHorario(TimeSpan hora)
        {
            if (hora < new TimeSpan(12, 0, 0))
            {
                return TipoHorario.Entrada;
            }
            else if (hora < new TimeSpan(13, 0, 0))
            {
                return TipoHorario.SaidaAlmoco;
            }
            else if (hora < new TimeSpan(18, 0, 0))
            {
                return TipoHorario.EntradaAlmoco;
            }
            else
            {
                return TipoHorario.Saida;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var horario = (Horario)obj;
            return DataHora == horario.DataHora && Tipo == horario.Tipo;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DataHora, Tipo);
        }
    }

    public enum TipoHorario
    {
        Entrada,
        SaidaAlmoco,
        EntradaAlmoco,
        Saida
    }
}
