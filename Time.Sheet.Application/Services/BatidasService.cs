using Time.Sheet.Domain.Models;  
using Time.Sheet.Domain.Services;
using Time.Sheet.Domain.Utils;
using Time.Sheet.Infraestructure.Repositories;

namespace Time.Sheet.Application.Services
{
    public class BatidasService : IBatidasService
    {
        private readonly IRegistrosRepository _registrosRepository;

        public BatidasService(IRegistrosRepository registrosRepository)
        {
            _registrosRepository = registrosRepository;
        }

        public async Task<ResponseResult> InserirBatidaAsync(Momento momento)
        {

            if (momento == null || string.IsNullOrEmpty(momento.DataHora))
            {
                return new ResponseResult(400, "Campo obrigatório não informado");
            }

            DateTime dataHora;

            if (!DateTime.TryParse(momento.DataHora, out dataHora))
            {
                return new ResponseResult(400, "Data e hora em formato inválido");
            }

            if (dataHora.DayOfWeek == DayOfWeek.Saturday || dataHora.DayOfWeek == DayOfWeek.Sunday)
            {
                return new ResponseResult(403, "Sábado e domingo não são permitidos como dia de trabalho");
            }

            DateTime dia = dataHora.Date;
            TimeSpan hora = dataHora.TimeOfDay;

            Registro registro = await _registrosRepository.ObterRegistroPorDiaAsync(dia);

            if (registro == null)
            {
                registro = new Registro { Dia = dia, Horarios = new Horario[4] };
            }
            else
            {
                if (registro.Horarios.Any(h => h.DataHora == hora))
                {
                    return new ResponseResult(409, "Horário já registrado");
                }

                if (registro.Horarios.Count(h => h != null) >= 4)
                {
                    return new ResponseResult(403, "Apenas 4 horários podem ser registrados por dia");
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (registro.Horarios[i] == null)
                {
                    registro.Horarios[i] = new Horario { DataHora = hora };
                    break;
                }
            }

            if (registro.Id == 0)
            {
                await _registrosRepository.InserirAsync(registro);
            }
            else
            {
                await _registrosRepository.UpdateAsync(registro);
            }

            return new ResponseResult(201, "Created");
        }
    }
}
