using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time.Sheet.Domain.Models;
using Time.Sheet.Domain.Repositories;

namespace Time.Sheet.Domain.Services
{
    public class FolhasDePontoService : IFolhasDePontoService
    {
        private readonly IRegistrosRepository _registrosRepository;

        public FolhasDePontoService(IRegistrosRepository registrosRepository)
        {
            _registrosRepository = registrosRepository ?? throw new ArgumentNullException(nameof(registrosRepository));
        }

        public Relatorio GeraRelatorioMensal(string mes)
        {
            var registroList = _registrosRepository.ObtemRegistrosPorMes(mes);
            if (registroList == null || !registroList.Any())
            {
                return null;
            }

            var horasTrabalhadas = CalculaHorasTrabalhadas(registroList);
            var horasExcedentes = CalculaHorasExcedentes(horasTrabalhadas);
            var horasDevidas = CalculaHorasDevidas(horasTrabalhadas);

            var relatorio = new Relatorio
            {
                Mes = mes,
                HorasTrabalhadas = horasTrabalhadas.ToString(),
                HorasExcedentes = horasExcedentes.ToString(),
                HorasDevidas = horasDevidas.ToString(),
                Registros = registroList
            };

            return relatorio;
        }

        private TimeSpan CalculaHorasTrabalhadas(List<Registro> registros)
        {
            TimeSpan horasTrabalhadas = TimeSpan.Zero;
            foreach (var registro in registros)
            {
                horasTrabalhadas += CalculaHorasTrabalhadas(registro.Horarios);
            }
            return horasTrabalhadas;
        }

        private TimeSpan CalculaHorasExcedentes(TimeSpan horasTrabalhadas)
        {
            var jornadaDiaria = new TimeSpan(8, 0, 0);
            return horasTrabalhadas > jornadaDiaria ? horasTrabalhadas - jornadaDiaria : TimeSpan.Zero;
        }

        private TimeSpan CalculaHorasDevidas(TimeSpan horasTrabalhadas)
        {
            var jornadaMensal = new TimeSpan(160, 0, 0);
            return horasTrabalhadas < jornadaMensal ? jornadaMensal - horasTrabalhadas : TimeSpan.Zero;
        }

        private TimeSpan CalculaHorasTrabalhadas(List<string> horarios)
        {
            if (horarios == null || !horarios.Any())
            {
                return TimeSpan.Zero;
            }

            TimeSpan horasTrabalhadas = TimeSpan.Zero;
            var entradas = new List<DateTime>();
            var saidas = new List<DateTime>();
            foreach (var horario in horarios)
            {
                if (DateTime.TryParse(horario, out DateTime horarioDt))
                {
                    if (horarioDt.Hour < 12)
                    {
                        entradas.Add(horarioDt);
                    }
                    else
                    {
                        saidas.Add(horarioDt);
                    }
                }
            }
            for (int i = 0; i < entradas.Count; i++)
            {
                horasTrabalhadas += saidas[i] - entradas[i];
            }
            return horasTrabalhadas;
        }
    }
}
