using Time.Sheet.Domain.Models;
using Time.Sheet.Domain.Repositories;
using Time.Sheet.Domain.Utils;

namespace Time.Sheet.Domain.Services
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
                return new ResponseResult(400,"Data e hora em formato inválido");
            }

            if (dataHora.DayOfWeek == DayOfWeek.Saturday || dataHora.DayOfWeek == DayOfWeek.Sunday)
            {
                return new ResponseResult(403,"Sábado e domingo não são permitidos como dia de trabalho");
            }

            DateTime dia = dataHora.Date;
            TimeSpan hora = dataHora.TimeOfDay;

            Registro registro = await _registrosRepository.ObterRegistroPorDiaAsync(dia);

            if (registro == null)
            {
                registro = new Registro { Dia = dia };
            }
            else
            {
                if (registro.Horarios.Any(h => h.DataHora == hora))
                {
                    return new ResponseResult(409,"Horário já registrado");
                }

                if (registro.Horarios.Count >= 4)
                {
                    return new ResponseResult(403,"Apenas 4 horários podem ser registrados por dia" );
                }
            }

            Horario novoHorario = new Horario(hora);
            registro.Horarios.Add(novoHorario);

            if (registro.Horarios.Count >= 2 &&
                registro.Horarios.Any(h => h.Tipo == TipoHorario.SaidaAlmoco) &&
                registro.Horarios.Any(h => h.Tipo == TipoHorario.EntradaAlmoco) &&
                novoHorario.Tipo == TipoHorario.EntradaAlmoco)
            {
                TimeSpan intervaloAlmoco = registro.Horarios.First(h => h.Tipo == TipoHorario.EntradaAlmoco).DataHora -
                                           registro.Horarios.First(h => h.Tipo == TipoHorario.SaidaAlmoco).DataHora;
                if (intervaloAlmoco < TimeSpan.FromHours(1))
                {
                    registro.Horarios.Remove(novoHorario);
                    return new ResponseResult(403, "Deve haver no mínimo 1 hora de almoço");
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
