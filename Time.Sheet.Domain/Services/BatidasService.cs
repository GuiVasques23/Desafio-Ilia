using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time.Sheet.Domain.Models;
using Time.Sheet.Domain.Repositories;

namespace Time.Sheet.Domain.Services
{
    public class BatidasService : IBatidasService
    {
        private readonly IRegistrosRepository _registrosRepository;

        public BatidasService(IRegistrosRepository registrosRepository)
        {
            _registrosRepository = registrosRepository;
        }

        public async Task<Registro> InserirBatidaAsync(Momento momento)
        {
            var dataHora = DateTime.Parse(momento.DataHora);
            var horario = new Horario(dataHora.TimeOfDay);
            var registro = await _registrosRepository.ObterRegistroPorDiaAsync(dataHora.Date);
            if (registro == null)
            {
                registro = new Registro { Dia = dataHora.Date };
                registro.Horarios.Add(horario);
                await _registrosRepository.InserirAsync(registro);
            }
            else
            {
                var ultimoHorario = registro.Horarios.LastOrDefault();
                if (ultimoHorario != null && ultimoHorario.Equals(horario))
                {
                    throw new ArgumentException("Horário já registrado");
                }
                else if (registro.Horarios.Count >= 4)
                {
                    throw new ArgumentException("Apenas 4 horários podem ser registrados por dia");
                }
                else if (horario.Tipo == TipoHorario.Entrada && ultimoHorario != null && ultimoHorario.Tipo != TipoHorario.Saida)
                {
                    throw new ArgumentException("Deve haver no mínimo 1 hora de almoço");
                }
                else if (dataHora.DayOfWeek == DayOfWeek.Saturday || dataHora.DayOfWeek == DayOfWeek.Sunday)
                {
                    throw new ArgumentException("Sábado e domingo não são permitidos como dia de trabalho");
                }
                else
                {
                    registro.Horarios.Add(horario);
                    await _registrosRepository.AtualizarAsync(registro);
                }
            }
            return registro;
        }

        public async Task<Registro> ObterRegistroPorIdAsync(int id)
        {
            var registro = await _registrosRepository.ObterPorIdAsync(id);
            return registro;
        }
    }
}
