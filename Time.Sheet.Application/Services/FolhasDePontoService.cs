using Microsoft.EntityFrameworkCore;
using Time.Sheet.Domain.Models;
using Time.Sheet.Infraestructure.Repositories;

namespace Time.Sheet.Domain.Services
{
    public class FolhasDePontoService : IFolhasDePontoService
    {
        private readonly IRegistrosRepository _registrosRepository;

        public FolhasDePontoService(IRegistrosRepository registrosRepository)
        {
            _registrosRepository = registrosRepository ?? throw new ArgumentNullException(nameof(registrosRepository));
        }

        public async Task<Relatorio> GerarRelatorioMensalAsync(string mesReferencia)
        {
            // Obter registros do mês de referência
            var registros = await _registrosRepository.ObterRegistrosMensaisAsync(mesReferencia);

            if (registros == null)
            {
                return null;
            }

            // Cálculo das horas trabalhadas, excedentes e devidas
            TimeSpan horasTrabalhadas = TimeSpan.Zero;
            TimeSpan horasExcedentes = TimeSpan.Zero;
            TimeSpan horasDevidas = TimeSpan.Zero;
            TimeSpan horasDiarias = TimeSpan.FromHours(10);
            TimeSpan horasAlmoco = TimeSpan.FromHours(1);

            foreach (var registro in registros)
            {
                var diaAtual = DateTime.Today;
                var registrosDia = registros.Where(r => r.Dia == diaAtual).ToList();

                // Calcular horas trabalhadas e excedentes
                var horarios = registrosDia[0].Horarios;
                if (horarios[0] != null && horarios[1] != null && horarios[2] != null && horarios[3] != null)
                {
                    var horasTrabalhadasDia = (horarios[3].DataHora - horarios[2].DataHora) + (horarios[1].DataHora - horarios[0].DataHora) - TimeSpan.FromHours(2);
                    if (horasTrabalhadasDia > TimeSpan.FromHours(10))
                    {
                        horasExcedentes += horasTrabalhadasDia - TimeSpan.FromHours(10);
                        horasTrabalhadas += TimeSpan.FromHours(10);
                    }
                    else
                    {
                        horasTrabalhadas += horasTrabalhadasDia;
                    }
                }
            }

            // Calcular horas devidas
            horasDevidas = TimeSpan.FromHours(220) - horasTrabalhadas;

            // Criar objeto relatório
            var relatorio = new Relatorio
            {
                Mes = mesReferencia,
                HorasTrabalhadas = horasTrabalhadas.ToString(),
                HorasExcedentes = horasExcedentes.ToString(),
                HorasDevidas = horasDevidas.ToString(),
                Registros = registros.ToList(),
            };

            return relatorio;
        }
    }
}
        
    

