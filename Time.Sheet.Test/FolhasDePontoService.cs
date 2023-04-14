using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Time.Sheet.Application.Services;
using Time.Sheet.Domain.Models;
using Time.Sheet.Domain.Services;
using Time.Sheet.Infraestructure.Repositories;

namespace Time.Sheet.Test
{
    public class FolhasDePontoServiceTests
    {
        private readonly IFolhasDePontoService _folhasDePontoService;
        private readonly IRegistrosRepository _registrosRepository;

        public FolhasDePontoServiceTests()
        {
            _registrosRepository = new RegistrosRepository();
            _folhasDePontoService = new FolhasDePontoService(_registrosRepository);
        }

        //[Fact]
        //public async Task GerarRelatorioMensal_DeveRetornarRelatorioMensalQuandoExistemRegistrosParaOMes()
        //{
        //    // Arrange
        //    var mes = "2023-04";
        //    var registros = new List<Registro>
        //    {
        //        new Registro
        //        {
        //            Dia = "2023-04-01",
        //            Horarios = new Horario[]
        //            {
        //                DateTime.Parse("2023-04-01T08:00:00"),
        //                DateTime.Parse("2023-04-01T12:00:00"),
        //                DateTime.Parse("2023-04-01T13:00:00"),
        //                DateTime.Parse("2023-04-01T18:00:00")
        //            }
        //        },
        //        new Registro
        //        {
        //            Dia = "2023-04-02",
        //            Horarios = new Horario[]
        //            {
        //                DateTime.Parse("2023-04-02T08:00:00"),
        //                DateTime.Parse("2023-04-02T12:00:00"),
        //                DateTime.Parse("2023-04-02T13:00:00"),
        //                DateTime.Parse("2023-04-02T18:00:00")
        //            }
        //        }
        //    };
        //    foreach (var registro in registros)
        //    {
        //        await _registrosRepository.InserirRegistroAsync(registro);
        //    }

        //    var horasTrabalhadas = TimeSpan.FromHours(16);
        //    var horasDevidas = TimeSpan.Zero;
        //    var horasExcedentes = TimeSpan.Zero;

        //    // Act
        //    var response = await _folhasDePontoService.GerarRelatorioMensalAsync(mes);

        //    // Assert
        //    Assert.NotNull(response);
        //    Assert.Equal(mes, response.Mes);
        //    Assert.Equal(horasTrabalhadas, response.HorasTrabalhadas);
        //    Assert.Equal(horasDevidas, response.HorasDevidas);
        //    Assert.Equal(horasExcedentes, response.HorasExcedentes);
        //    Assert.Equal(registros.Count, response.Registros.Count);
        //}
    }
}